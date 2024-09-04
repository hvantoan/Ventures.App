using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Domain.ExternalServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CB.Application.Handlers.AuthHandlers.Commands;

public class LoginCommand : IRequest<LoginResult> {
    public string MerchantCode { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public EPermission Permission { get; set; }
}

public class LoginResult {
    public string RefreshToken { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string MerchantCode { get; set; } = string.Empty;
    public string MerchantName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public long ExpiredTime { get; set; }
    public long Session { get; set; }
}

public class LoginHandler(IServiceProvider serviceProvider) : BaseHandler<LoginCommand, LoginResult>(serviceProvider) {
    private readonly IRedisService redisCacheService = serviceProvider.GetRequiredService<IRedisService>();

    public override async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken) {
        request.Username = request.Username.ToLower().Trim();
        return await this.Login(request, request.Permission, cancellationToken);
    }

    public async Task<LoginResult> Login(LoginCommand request, EPermission type, CancellationToken cancellationToken) {
        var merchant = await this.db.Merchants.AsNoTracking().FirstOrDefaultAsync(o => o.Code == request.MerchantCode, cancellationToken);
        CbException.ThrowIfNull(merchant, Messages.Merchant_NotFound);
        CbException.ThrowIfFalse(merchant.IsActive, Messages.Merchant_Inactive);
        CbException.ThrowIf(merchant.ExpiredDate <= DateTimeOffset.UtcNow, Messages.Merchant_Expired);

        var user = await this.db.Users.FirstOrDefaultAsync(o => o.MerchantId == merchant.Id && o.Username == request.Username, cancellationToken);
        CbException.ThrowIfNull(user, Messages.User_NotFound);
        CbException.ThrowIf(!user.IsAdmin && !user.IsActive, Messages.User_Inactive);
        CbException.ThrowIf(!PasswordHasher.Verify(request.Password, user.Password), Messages.User_IncorrectPassword);

        var permissions = await this.db.GetPermissions(o => o.Type == type, cancellationToken);
        Role? role = null;
        if (!string.IsNullOrWhiteSpace(user.RoleId)) {
            role = await this.db.Roles.Include(o => o.RolePermissions).AsNoTracking().AsSplitQuery()
                .FirstOrDefaultAsync(o => o.Id == user.RoleId && o.MerchantId == merchant.Id, cancellationToken);
        }

        var userPermissions = UserPermissionDto.MapFromEntities(permissions, role?.RolePermissions?.ToList(), user.IsAdmin);
        CbException.ThrowIfFalse(userPermissions.Exists(o => o.IsEnable), Messages.User_NoPermission);

        var claims = new List<Claim>() {
            new(Constants.TokenMerchantId, merchant.Id),
            new(Constants.TokenUserId, user.Id),
        };

        if (user.IsAdmin) claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var session = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string source = type.ToString();

        claims.Add(new Claim(Constants.TokenSource, source));
        claims.Add(new Claim(Constants.TokenSession, session.ToString()));

        user.LastSession = session;
        await this.db.SaveChangesAsync(cancellationToken);

        var permissionClaims = this.GetClaimPermissions(userPermissions);
        claims.AddRange(permissionClaims);

        var expiredAt = this.GetTokenExpiredAt();
        var expiredTime = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds();
        var ttlKey = TimeSpan.FromMilliseconds(expiredTime - session);

        string cacheKey = RedisKey.GetSessionKey(source, user.Id);
        await this.redisCacheService.SetAsync(cacheKey, session, ttlKey);

        return new() {
            RefreshToken = this.GenerateRefreshToken(claims),
            Token = this.GenerateToken(claims, expiredAt),
            ExpiredTime = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds(),
            MerchantCode = merchant.Code,
            MerchantName = merchant.Name,
            Username = user.Username,
            Name = user.Name,
            Session = session,
        };
    }

    private DateTime GetTokenExpiredAt() {
        long tokenExpiredAfterDays = this.configuration.GetSection("TokenExpire").Get<long?>() ?? 1;
        return DateTimeOffset.Now.AddDays(tokenExpiredAfterDays).Date;
    }

    private string GenerateRefreshToken(List<Claim> claims) {
        var claimsForRefreshToken = new List<Claim>();
        claimsForRefreshToken.AddRange(claims);
        claimsForRefreshToken.Add(new Claim(Constants.TokenRefreshToken, "true"));

        return this.GenerateToken(claimsForRefreshToken, DateTime.Now.AddYears(10));
    }

    private string GenerateToken(List<Claim> claims, DateTime expiredAt) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtSecret"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          claims: claims,
          expires: expiredAt,
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> GetClaimPermissions(List<UserPermissionDto> permissions) {
        List<Claim> claims = new();
        foreach (var item in permissions) {
            if (!item.IsEnable) continue;
            if (item.IsClaim)
                claims.Add(new Claim(ClaimTypes.Role, item.ClaimName));

            if (item.Items != null && item.Items.Any())
                claims.AddRange(this.GetClaimPermissions(item.Items));
        }
        return claims;
    }
}

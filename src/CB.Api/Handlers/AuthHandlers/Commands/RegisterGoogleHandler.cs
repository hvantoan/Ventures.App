using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Domain.ExternalServices.Interfaces;
using CB.Domain.ExternalServices.Models;
using CB.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using UserPermissionDto = CB.Api.Models.UserPermissionDto;

namespace CB.Api.Handlers.AuthHandlers.Commands;

public class RegisterGoogleCommand : IRequest<LoginResult> {
    public string Id { get; set; } = string.Empty;
    public string MerchantCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}

public class RegisterGoogleHandler(IServiceProvider serviceProvider) : Common.BaseHandler<RegisterGoogleCommand, LoginResult>(serviceProvider) {
    private readonly IRedisService redisCacheService = serviceProvider.GetRequiredService<IRedisService>();
    private readonly IImageService imageService = serviceProvider.GetRequiredService<IImageService>();
    public override async Task<LoginResult> Handle(RegisterGoogleCommand request, CancellationToken cancellationToken) {
        var merchant = await this.db.Merchants.FirstOrDefaultAsync(o => o.Code == request.MerchantCode, cancellationToken);
        CbException.ThrowIfNull(merchant, Messages.Merchant_NotFound);

        var roleDefault = await this.db.Roles.FirstOrDefaultAsync(o => o.MerchantId == merchant.Id && o.IsClient, cancellationToken);
        if (roleDefault == null) {
            roleDefault = new Role() {
                Id = NGuidHelper.New(),
                Code = "RO_NguoiDung",
                Name = "Người dùng",
                CreatedDate = DateTimeOffset.Now,
                IsClient = true,
                MerchantId = merchant.Id,
                SearchName = "nguoi dung",
            };
            await this.db.Roles.AddAsync(roleDefault, cancellationToken);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        var user = await db.Users.FirstOrDefaultAsync(o => o.Username == request.Email, cancellationToken);
        if (user == null) {
            user ??= new User {
                Id = NGuidHelper.New(),
                Username = request.Email,
                MerchantId = merchant.Id,
                Password = PasswordHasher.Hash(""),
                Name = request.Name,
                SearchName = StringHelper.UnsignedUnicode(request.Name),
                IsActive = true,
                Provider = EProvider.Google,
                Avatar = request.Image,
                RoleId = roleDefault.Id,
            };
            this.db.Users.Add(user);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        var permissions = await db.GetPermissions(cancellationToken: cancellationToken);
        Role? role = null;
        if (user.RoleId != null) {
            role = await db.Roles.Include(o => o.RolePermissions).FirstOrDefaultAsync(o => o.Id == user.RoleId, cancellationToken);
        }

        var userPermissions = UserPermissionDto.MapFromEntities(permissions, role?.RolePermissions?.ToList(), user.IsAdmin);
        CbException.ThrowIfFalse(userPermissions.Exists(o => o.IsEnable), Messages.User_NoPermission);

        var claims = new List<Claim>() {
            new(Constants.TokenMerchantId, merchant.Id),
            new(Constants.TokenUserId, user.Id.ToString()),
        };


        if (user.IsAdmin) claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var session = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string source = EPermission.Web.ToString();

        claims.Add(new Claim(Constants.TokenSource, source));
        claims.Add(new Claim(Constants.TokenSession, session.ToString()));

        user.LastSession = session;
        await this.db.SaveChangesAsync(cancellationToken);

        var permissionClaims = GetClaimPermissions(userPermissions);
        claims.AddRange(permissionClaims);

        var expiredAt = this.GetTokenExpiredAt();
        var expiredTime = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds();
        var ttlKey = TimeSpan.FromMilliseconds(expiredTime - session);

        string cacheKey = RedisKey.GetSessionKey(source, user.Id);
        await this.redisCacheService.SetAsync(cacheKey, session, ttlKey);

        var avatars = await imageService.List(merchant.Id, EItemImage.UserAvatar, user.Id);
        return new() {
            RefreshToken = GenerateRefreshToken(claims),
            Token = GenerateToken(claims, expiredAt),
            ExpiredTime = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds(),
            Username = user.Username,
            Name = user.Name,
            MerchantCode = merchant.Code,
            MerchantName = merchant.Name,
            Avatar = user.Avatar ?? ImageDto.FromEntity(avatars.FirstOrDefault(), url)?.Image,
            Session = session,
        };
    }

    private DateTime GetTokenExpiredAt() {
        long tokenExpiredAfterDays = configuration.GetSection("TokenExpire").Get<long?>() ?? 1;
        return DateTimeOffset.Now.AddDays(tokenExpiredAfterDays).Date;
    }

    private string GenerateRefreshToken(List<Claim> claims) {
        var claimsForRefreshToken = new List<Claim>();
        claimsForRefreshToken.AddRange(claims);
        claimsForRefreshToken.Add(new Claim(Constants.TokenRefreshToken, "true"));

        return GenerateToken(claimsForRefreshToken, DateTime.Now.AddYears(10));
    }

    private string GenerateToken(List<Claim> claims, DateTime expiredAt) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecret"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          claims: claims,
          expires: expiredAt,
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static List<Claim> GetClaimPermissions(List<UserPermissionDto> permissions) {
        List<Claim> claims = [];
        foreach (var item in permissions) {
            if (!item.IsEnable) continue;
            if (item.IsClaim)
                claims.Add(new Claim(ClaimTypes.Role, item.ClaimName));

            if (item.Items != null && item.Items.Count != 0)
                claims.AddRange(GetClaimPermissions(item.Items));
        }
        return claims;
    }
}

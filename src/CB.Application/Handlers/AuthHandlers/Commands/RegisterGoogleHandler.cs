using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CB.Application.Handlers.AuthHandlers.Commands;

public class RegisterGoogleCommand : IRequest<LoginResult> {
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}

public class RegisterGoogleHandler(IServiceProvider serviceProvider) : BaseHandler<RegisterGoogleCommand, LoginResult>(serviceProvider) {

    public override async Task<LoginResult> Handle(RegisterGoogleCommand request, CancellationToken cancellationToken) {
        var user = await db.Users.FirstOrDefaultAsync(o => o.Username == request.Email, cancellationToken);
        if (user == null) {
            user ??= new User {
                Id = Guid.NewGuid(),
                Username = request.Email,
                Password = PasswordHasher.Hash(""),
                Name = request.Name,
                IsActive = true,
                Provider = EProvider.Google,
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
            new(Constants.TokenUserId, user.Id.ToString()),
        };

        if (user.IsAdmin) claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var permissionClaims = GetClaimPermissions(userPermissions);
        claims.AddRange(permissionClaims);
        var expiredAt = GetTokenExpiredAt();

        return new() {
            RefreshToken = GenerateRefreshToken(claims),
            Token = GenerateToken(claims, expiredAt),
            ExpiredTime = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds(),
            Username = user.Username,
            Name = user.Name,
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

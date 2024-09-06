using System.Diagnostics.CodeAnalysis;
using CB.Domain.Common.Resource;
using CB.Domain.Extentions;

namespace CB.Application.Models;

public class UserDto {
    public string? Id { get; set; }
    public string? Username { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    public string? Password { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    public string? PinCode { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public Domain.Common.Resource.Unit? Province { get; set; }
    public Domain.Common.Resource.Unit? District { get; set; }
    public Domain.Common.Resource.Unit? Commune { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public bool IsAdmin { get; set; }

    public RoleDto? Role { get; set; }
    public List<BankCardDto>? BankCards { get; set; }

    [return: NotNullIfNotNull(nameof(entity))]
    public static UserDto? FromEntity(User? entity, UnitResource? unitRes, Role? roleEntity = null) {
        if (entity == null) return default;
        entity.Role ??= roleEntity;

        var au = unitRes?.GetByCode(entity.Province, entity.District, entity.Commune) ?? [];

        return new UserDto {
            Id = entity.Id,
            Username = entity.Username,
            Name = entity.Name,
            Phone = entity.Phone,
            Province = au.GetValue(entity.Province),
            District = au.GetValue(entity.District),
            Commune = au.GetValue(entity.Commune),
            Address = entity.Address,
            IsActive = entity.IsActive,
            IsAdmin = entity.IsAdmin,
            Role = RoleDto.FromEntity(entity.Role),
            BankCards = entity.BankCards?.Select(o => BankCardDto.FromEntity(o)).ToList(),
        };
    }
}

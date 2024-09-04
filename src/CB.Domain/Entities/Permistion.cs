using CB.Domain.Enums;

namespace CB.Domain.Entities;

public partial class Permission : ICloneable {
    public string Id { get; set; } = null!;
    public string? ParentId { get; set; }
    public string ClaimName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public bool IsClaim { get; set; }
    public int OrderIndex { get; set; }
    public EPermission Type { get; set; }

    public virtual ICollection<RolePermission>? RolePermissions { get; set; }

    public object Clone() {
        return new Permission {
            Id = Id,
            ParentId = ParentId,
            ClaimName = ClaimName,
            DisplayName = DisplayName,
            IsDefault = IsDefault,
            IsActive = IsActive,
            IsClaim = IsClaim,
            OrderIndex = OrderIndex,
            Type = Type,
        };
    }
}

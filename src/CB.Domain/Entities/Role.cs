using System.ComponentModel;
using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class Role : IEntity {
    public required string Id { get; set; }
    public required string MerchantId { get; set; }

    [Description("Mã phân quyền")]
    public required string Code { get; set; }

    [Description("Tên phân quyền")]
    public required string Name { get; set; }

    public required string SearchName { get; set; }

    public string? Description { get; set; }

    public bool IsClient { get; set; } = false;
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
    public bool IsDelete { get; set; }

    public virtual ICollection<User>? Users { get; set; }
    public virtual ICollection<RolePermission>? RolePermissions { get; set; }
}

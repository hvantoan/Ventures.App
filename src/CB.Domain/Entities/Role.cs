using System.ComponentModel;
using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class Role : IEntity {
    public string Id { get; set; } = null!;
    public string MerchantId { get; set; } = null!;

    [Description("Mã phân quyền")]
    public string Code { get; set; } = null!;

    [Description("Tên phân quyền")]
    public string Name { get; set; } = null!;

    public string SearchName { get; set; } = null!;
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    public bool IsDelete { get; set; }

    public virtual ICollection<User>? Users { get; set; }
    public virtual ICollection<RolePermission>? RolePermissions { get; set; }
}

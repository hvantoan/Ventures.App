using System.ComponentModel;
using CB.Domain.Common.Interfaces;
using CB.Domain.Enums;

namespace CB.Domain.Entities;

public partial class User : IEntity {
    public string Id { get; set; } = null!;
    public string MerchantId { get; set; } = null!;

    [Description("Phân quyền")]
    public string? RoleId { get; set; }

    [Description("Tên đăng nhập")]
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    [Description("Tên người dùng")]
    public string Name { get; set; } = null!;

    public string SearchName { get; set; } = null!;

    public string? IdentityCard { get; set; }

    [Description("Số điện thoại")]
    public string? Phone { get; set; }

    [Description("Email")]
    public string? Email { get; set; }

    [Description("Tỉnh/Thành phố")]
    public string? Province { get; set; }

    [Description("Quận/Huyện")]
    public string? District { get; set; }

    [Description("Phường/Xã")]
    public string? Commune { get; set; }

    [Description("Địa chỉ")]
    public string? Address { get; set; }

    [Description("Trạng thái")]
    public bool IsActive { get; set; }

    public EProvider Provider { get; set; }
    public string? ParentId { get; set; }

    public bool IsAdmin { get; set; }
    public bool IsSystem { get; set; }
    public bool IsDelete { get; set; }
    public long LastSession { get; set; }
    public string? Avatar { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<Contact>? Contacts { get; set; }
    public virtual ICollection<BankCard>? BankCards { get; set; }
    public virtual ICollection<UserBot>? UserBots { get; set; }
}

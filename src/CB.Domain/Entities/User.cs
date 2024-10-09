using System.ComponentModel;
using CB.Domain.Common.Interfaces;
using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class User : IEntity {
    public required string Id { get; set; }
    public required string MerchantId { get; set; }

    [Description("Phân quyền")]
    public string? RoleId { get; set; }

    [Description("Tên đăng nhập")]
    public required string Username { get; set; }

    public required string Password { get; set; }

    [Description("Tên người dùng")]
    public required string Name { get; set; }

    public required string SearchName { get; set; }

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

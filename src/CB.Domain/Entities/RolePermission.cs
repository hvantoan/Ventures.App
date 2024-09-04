namespace CB.Domain.Entities;

public class RolePermission {
    public string RoleId { get; set; } = null!;
    public string PermissionId { get; set; } = null!;
    public bool IsEnable { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Permission? Permission { get; set; }
}

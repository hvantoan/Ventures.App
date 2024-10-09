namespace CB.Domain.Entities;

public class RolePermission {
    public required string RoleId { get; set; }
    public required string PermissionId { get; set; }
    public bool IsEnable { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Permission? Permission { get; set; }
}

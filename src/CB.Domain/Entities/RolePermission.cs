﻿namespace CB.Domain.Entities;

public class RolePermission {
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public bool IsEnable { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Permission? Permission { get; set; }
}

namespace CB.Application.Handlers.RoleHandlers.Queries;

public class GetRoleQuery : SingleRequest<RoleDto?> { }

public class GetRoleHandler : BaseHandler<GetRoleQuery, RoleDto?> {

    public GetRoleHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public override async Task<RoleDto?> Handle(GetRoleQuery request, CancellationToken cancellationToken) {
        var role = await db.Roles.AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete)
            .FirstOrDefaultAsync(cancellationToken);
        if (role == null) return null;

        role.RolePermissions = await db.RolePermissions.AsNoTracking()
            .Where(o => o.RoleId == role.Id).ToListAsync(cancellationToken);

        var permissions = await db.GetPermissions(o => o.IsActive, cancellationToken);
        return RoleDto.FromEntity(role, permissions);
    }
}

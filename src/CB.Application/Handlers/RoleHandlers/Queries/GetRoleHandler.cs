using Models_RoleDto = CB.Application.Models.RoleDto;
using RoleDto = CB.Application.Models.RoleDto;

namespace CB.Application.Handlers.RoleHandlers.Queries;

public class GetRoleQuery : Common.SingleRequest<Models_RoleDto?> { }

public class GetRoleHandler : Common.BaseHandler<GetRoleQuery, Models_RoleDto?> {

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

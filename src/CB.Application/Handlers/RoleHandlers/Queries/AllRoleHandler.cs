using Models_RoleDto = CB.Application.Models.RoleDto;
using RoleDto = CB.Application.Models.RoleDto;

namespace CB.Application.Handlers.RoleHandlers.Queries;

public class AllRoleQuery : Common.Request<AllRoleData> {
}

public class AllRoleData : Common.PaginatedList<Models_RoleDto> {
}

public class AllRoleHandler : Common.BaseHandler<AllRoleQuery, AllRoleData> {

    public AllRoleHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public override async Task<AllRoleData> Handle(AllRoleQuery request, CancellationToken cancellationToken) {
        var items = await db.Roles.AsNoTracking().Where(o => o.MerchantId == request.MerchantId && !o.IsDelete)
            .OrderBy(o => o.Code).Select(o => RoleDto.FromEntity(o, null)!).ToListAsync(cancellationToken);
        return new() { Items = items, Count = items.Count };
    }
}

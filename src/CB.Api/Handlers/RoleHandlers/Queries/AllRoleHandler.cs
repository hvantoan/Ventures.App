using RoleDto = CB.Api.Models.RoleDto;

namespace CB.Api.Handlers.RoleHandlers.Queries;

public class AllRoleQuery : Common.Request<AllRoleData> {
}

public class AllRoleData : Common.PaginatedList<RoleDto> {
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

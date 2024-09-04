namespace CB.Application.Handlers.RoleHandlers.Queries;

public class AllRoleQuery : Request<AllRoleData> {
}

public class AllRoleData : PaginatedList<RoleDto> {
}

public class AllRoleHandler : BaseHandler<AllRoleQuery, AllRoleData> {

    public AllRoleHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public override async Task<AllRoleData> Handle(AllRoleQuery request, CancellationToken cancellationToken) {
        var items = await db.Roles.AsNoTracking().Where(o => o.MerchantId == request.MerchantId && !o.IsDelete)
            .OrderBy(o => o.Code).Select(o => RoleDto.FromEntity(o, null)!).ToListAsync(cancellationToken);
        return new() { Items = items, Count = items.Count };
    }
}

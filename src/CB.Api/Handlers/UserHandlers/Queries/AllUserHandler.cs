using UserDto = CB.Api.Models.UserDto;

namespace CB.Api.Handlers.UserHandlers.Queries;

public class AllUserQuery : Common.Request<ListUserData> { }

public class AllUserHandler(IServiceProvider serviceProvider) : Common.BaseHandler<AllUserQuery, ListUserData>(serviceProvider) {
    public override async Task<ListUserData> Handle(AllUserQuery request, CancellationToken cancellationToken) {
        var items = await db.Users.AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && !o.IsDelete && !o.IsSystem)
            .OrderBy(o => o.Username).Select(o => UserDto.FromEntity(o, null, null, null, null)).ToListAsync(cancellationToken);
        return new() { Items = items, Count = items.Count };
    }
}

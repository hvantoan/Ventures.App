using CB.Domain.Common.Resource;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.UserHandlers.Queries;

public class ListUserQuery : PaginatedRequest<ListUserData> { }

public class ListUserData : PaginatedList<UserDto> { }

public class ListUserHandler(IServiceProvider serviceProvider) : BaseHandler<ListUserQuery, ListUserData>(serviceProvider) {
    private readonly UnitResource unitRes = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<ListUserData> Handle(ListUserQuery request, CancellationToken cancellationToken) {
        var query = db.Users.Include(o => o.Role).AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && !o.IsDelete && !o.IsSystem)
            .WhereIf(!string.IsNullOrWhiteSpace(request.SearchText), o => o.Username.Contains(request.SearchText!.Trim().ToLower()));

        int count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken);

        UserDto? firstItem = null;
        if (string.IsNullOrWhiteSpace(request.SearchText) && !string.IsNullOrWhiteSpace(request.FirstItemId) && request.PageIndex == 0) {
            firstItem = await query.Where(o => o.Id == request.FirstItemId)
                .Select(o => UserDto.FromEntity(o, unitRes, null))
                .FirstOrDefaultAsync(cancellationToken);
            if (firstItem != null) {
                request.PageSize--;
                query = query.Where(o => o.Id != firstItem.Id);
            }
        }

        var items = await query.OrderByDescending(o => o.IsAdmin).ThenBy(o => o.Username)
        .Skip(request.PageIndex * request.PageSize).Take(request.PageSize)
            .Select(o => UserDto.FromEntity(o, unitRes, null)).ToListAsync(cancellationToken);

        if (firstItem != null) items.Insert(0, firstItem);

        return new() {
            Items = items!,
            Count = count,
        };
    }
}

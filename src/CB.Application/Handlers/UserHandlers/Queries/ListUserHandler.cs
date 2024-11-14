using CB.Domain.Common.Resource;
using CB.Domain.Extentions;
using Models_UserDto = CB.Application.Models.UserDto;
using UserDto = CB.Application.Models.UserDto;

namespace CB.Application.Handlers.UserHandlers.Queries;

public class ListUserQuery : Common.PaginatedRequest<ListUserData> { }

public class ListUserData : Common.PaginatedList<Models_UserDto> { }

public class ListUserHandler(IServiceProvider serviceProvider) : Common.BaseHandler<ListUserQuery, ListUserData>(serviceProvider) {
    private readonly UnitResource unitRes = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<ListUserData> Handle(ListUserQuery request, CancellationToken cancellationToken) {
        var query = db.Users.Include(o => o.Role).AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && !o.IsDelete && !o.IsSystem)
            .WhereIf(!string.IsNullOrWhiteSpace(request.SearchText), o => o.Username.Contains(request.SearchText!.Trim().ToLower()));

        int count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken);

        UserDto? firstItem = null;
        if (string.IsNullOrWhiteSpace(request.SearchText) && !string.IsNullOrWhiteSpace(request.FirstItemId) && request.PageIndex == 0) {
            firstItem = await query.Where(o => o.Id == request.FirstItemId)
                .Select(o => UserDto.FromEntity(o, unitRes, null, null, null))
                .FirstOrDefaultAsync(cancellationToken);
            if (firstItem != null) {
                request.PageSize--;
                query = query.Where(o => o.Id != firstItem.Id);
            }
        }

        var items = await query.OrderByDescending(o => o.IsAdmin).ThenBy(o => o.Username)
            .WhereFunc(!request.IsAll, q => q.Skip(request.PageIndex * request.PageSize).Take(request.PageSize))
            .Select(o => UserDto.FromEntity(o, unitRes, null, null, null)).ToListAsync(cancellationToken);

        if (firstItem != null) items.Insert(0, firstItem);

        return new() {
            Items = items!,
            Count = count,
        };
    }
}

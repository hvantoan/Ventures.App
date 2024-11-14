using CB.Domain.Extentions;
using RoleDto = CB.Api.Models.RoleDto;

namespace CB.Api.Handlers.RoleHandlers.Queries;

public class ListRoleQuery : Common.PaginatedRequest<ListRoleData> { }

public class ListRoleData : Common.PaginatedList<RoleDto> { }

public class ListRoleHandler : Common.BaseHandler<ListRoleQuery, ListRoleData> {

    public ListRoleHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public override async Task<ListRoleData> Handle(ListRoleQuery request, CancellationToken cancellationToken) {
        var query = db.Roles.AsNoTracking().Where(o => o.MerchantId == request.MerchantId && !o.IsDelete)
            .WhereFunc(!string.IsNullOrWhiteSpace(request.SearchText), q => {
                var searchText = request.SearchText!.ToLowerInvariant().Trim().ToLower();
                return q.Where(o => o.Code.Contains(searchText.ToUpper()) || o.SearchName.Contains(searchText));
            });

        int count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken);

        RoleDto? firstItem = null;
        if (string.IsNullOrWhiteSpace(request.SearchText) && !string.IsNullOrWhiteSpace(request.FirstItemId) && request.PageIndex == 0) {
            firstItem = await query.Where(o => o.Id == request.FirstItemId)
                .Select(o => RoleDto.FromEntity(o, null))
                .FirstOrDefaultAsync(cancellationToken);
            if (firstItem != null) {
                request.PageSize--;
                query = query.Where(o => o.Id != firstItem.Id);
            }
        }

        var items = await query.OrderBy(o => o.Code)
            .Skip(request.PageIndex * request.PageSize).Take(request.PageSize)
            .Select(o => RoleDto.FromEntity(o, null)!).ToListAsync(cancellationToken);

        if (firstItem != null) items.Insert(0, firstItem);

        return new() {
            Items = items,
            Count = count,
        };
    }
}

using CB.Domain.Extentions;

namespace CB.Application.Handlers.ContactHandlers.Queries;

public class ListContactQuery : PaginatedRequest<ListContact> { }

public class ListContact : PaginatedList<ContactDto> { }

internal class ListContactHandler(IServiceProvider serviceProvider) : BaseHandler<ListContactQuery, ListContact>(serviceProvider) {

    public override async Task<ListContact> Handle(ListContactQuery request, CancellationToken cancellationToken) {
        var query = this.db.Contacts
            .Include(o => o.BankCard)
            .WhereIf(!string.IsNullOrEmpty(request.SearchText), o =>
                        o.SearchName.Contains(request.SearchText!)
                     || (!string.IsNullOrEmpty(o.Email) && o.Email.Contains(request.SearchText!))
                     || (!string.IsNullOrEmpty(o.Phone) && o.Phone.Contains(request.SearchText!))
            );

        return new ListContact {
            Count = await query.CountAsync(cancellationToken),
            Items = await query.Skip(request.Skip).Take(request.Take).Select(o => ContactDto.FromEntity(o, o.BankCard, null, null, null, null, null)).ToListAsync(cancellationToken)
        };
    }
}

using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.ContactHandlers.Queries;

public class GetContactCommand : SingleRequest<ContactDto> {
}

internal class GetContactHandler(IServiceProvider serviceProvider) : BaseHandler<GetContactCommand, ContactDto>(serviceProvider) {

    public override async Task<ContactDto> Handle(GetContactCommand request, CancellationToken cancellationToken) {
        var contact = await this.db.Contacts.Include(o => o.BankCard)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        CbException.ThrowIfNull(contact, Messages.Contact_NotFound);
        return ContactDto.FromEntity(contact, contact.BankCard);
    }
}

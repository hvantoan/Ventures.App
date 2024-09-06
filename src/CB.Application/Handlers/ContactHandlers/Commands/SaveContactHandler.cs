using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.ContactHandlers.Commands;

public class SaveContactCommand : ModelRequest<ContactDto, string> { }

public class SaveContactHandler(IServiceProvider serviceProvider) : BaseHandler<SaveContactCommand, string>(serviceProvider) {

    public override async Task<string> Handle(SaveContactCommand request, CancellationToken cancellationToken) {
        var userId = request.UserId;
        var model = request.Model;
        model.Name = model.Name.Trim();

        return string.IsNullOrWhiteSpace(model.Id)
            ? await Create(userId!, model, cancellationToken)
            : await Update(userId!, model, cancellationToken);
    }

    private async Task<string> Create(string userId, ContactDto model, CancellationToken cancellationToken) {
        Contact contact = model.ToEntity();
        contact.UserId = userId;
        await db.Contacts.AddAsync(contact, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return contact.Id;
    }

    private async Task<string> Update(string userId, ContactDto model, CancellationToken cancellationToken) {
        var contact = await db.Contacts.Include(o => o.BankCard)
            .AsTracking().FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId, cancellationToken);
        CbException.ThrowIfNull(contact, Messages.Contact_NotFound);

        contact.FromUpdate(model.ToEntity());
        await db.Contacts.AddAsync(contact, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return contact.Id;
    }
}

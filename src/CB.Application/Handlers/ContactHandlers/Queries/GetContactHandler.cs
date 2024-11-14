using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using ContactDto = CB.Application.Models.ContactDto;
using Models_ContactDto = CB.Application.Models.ContactDto;

namespace CB.Application.Handlers.ContactHandlers.Queries;

public class GetContactCommand : Common.SingleRequest<Models_ContactDto> {
}

internal class GetContactHandler(IServiceProvider serviceProvider) : Common.BaseHandler<GetContactCommand, Models_ContactDto>(serviceProvider) {

    public override async Task<ContactDto> Handle(GetContactCommand request, CancellationToken cancellationToken) {
        var contact = await this.db.Contacts.Include(o => o.BankCard)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        CbException.ThrowIfNull(contact, Messages.Contact_NotFound);

        // Get images (logo)
        var typeInclued = new List<EItemImage> { EItemImage.UserFontBankCard, EItemImage.UserBackBankCard, EItemImage.UserFontIdentityCard, EItemImage.UserBackIdentityCard };
        var images = await this.db.ItemImages.AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && (o.ItemId == contact.Id || o.ItemId == contact.BankCard!.Id))
            .Where(o => typeInclued.Contains(o.ItemType))
            .GroupBy(o => o.ItemType)
            .ToDictionaryAsync(o => o.Key, v => v.ToList(), cancellationToken);
        var fontBank = images.GetValueOrDefault(EItemImage.UserFontBankCard)?.FirstOrDefault();
        var backBank = images.GetValueOrDefault(EItemImage.UserBackBankCard)?.FirstOrDefault();
        var fontIdCard = images.GetValueOrDefault(EItemImage.UserFontIdentityCard)?.FirstOrDefault();
        var backIdCard = images.GetValueOrDefault(EItemImage.UserBackIdentityCard)?.FirstOrDefault();

        return ContactDto.FromEntity(contact, contact.BankCard, url, fontBank, backBank, fontIdCard, backIdCard);
    }
}

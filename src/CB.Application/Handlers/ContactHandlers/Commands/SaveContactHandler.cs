using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Infrastructure.Services.Interfaces;

namespace CB.Application.Handlers.ContactHandlers.Commands;

public class SaveContactCommand : ModelRequest<ContactDto, string> { }

public class SaveContactHandler(IServiceProvider serviceProvider) : BaseHandler<SaveContactCommand, string>(serviceProvider) {
    private readonly IImageService imageService = serviceProvider.GetRequiredService<IImageService>();

    public override async Task<string> Handle(SaveContactCommand request, CancellationToken cancellationToken) {
        var merchantId = request.MerchantId;
        var userId = request.UserId;
        var model = request.Model;
        model.Name = model.Name.Trim();

        return string.IsNullOrWhiteSpace(model.Id)
            ? await Create(merchantId, userId!, model, cancellationToken)
            : await Update(merchantId, userId!, model, cancellationToken);
    }

    private async Task<string> Create(string merchantId, string userId, ContactDto model, CancellationToken cancellationToken) {
        var contact = new Contact {
            Id = NGuidHelper.New(model.Id),
            UserId = userId,
            Name = model.Name,
            SearchName = StringHelper.UnsignedUnicode(model.Name),
            Address = model.Address,
            Email = model.Email,
            Phone = model.Phone,
            IdentityCard = model.IdentityCard,
            CreateAt = DateTime.UtcNow,
            BankCardId = string.Empty,
            BankCard = model.BankCard?.ToEntity(userId)
        };

        await db.Contacts.AddAsync(contact, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        if (model.FrontIdentityCard.Data != null && model.FrontIdentityCard.Data.Length > 0) {
            await this.imageService.Save(merchantId, EItemImage.UserFontIdentityCard, contact.Id!, model.FrontIdentityCard);
        }

        if (model.BackIdentityCard.Data != null && model.BackIdentityCard.Data.Length > 0) {
            await this.imageService.Save(merchantId, EItemImage.UserBackIdentityCard, contact.Id!, model.BackIdentityCard);
        }

        if (model.BankCard?.FrontBankCard.Data != null && model.BankCard?.FrontBankCard.Data.Length > 0) {
            await this.imageService.Save(merchantId, EItemImage.UserFontBankCard, contact.BankCard!.Id!, model.BankCard.FrontBankCard);
        }

        if (model.BankCard?.BackBankCard.Data != null && model.BankCard?.BackBankCard.Data.Length > 0) {
            await this.imageService.Save(merchantId, EItemImage.UserBackBankCard, contact.BankCard!.Id!, model.BankCard.BackBankCard);
        }

        return contact.Id;
    }

    private async Task<string> Update(string merchantId, string userId, ContactDto model, CancellationToken cancellationToken) {
        var contact = await db.Contacts.Include(o => o.BankCard)
            .AsTracking().FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId, cancellationToken);

        CbException.ThrowIfNull(contact, Messages.Contact_NotFound);
        contact.FromUpdate(model.ToEntity());
        await db.Contacts.AddAsync(contact, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        if (model.FrontIdentityCard.Data != null && model.FrontIdentityCard.Data.Length > 0) {
            var images = await this.imageService.List(merchantId, EItemImage.UserFontIdentityCard, model.Id!, false);
            await this.imageService.Save(merchantId, EItemImage.UserFontIdentityCard, model.Id!, model.FrontIdentityCard, entity: images.FirstOrDefault());
        }

        if (model.BackIdentityCard.Data != null && model.BackIdentityCard.Data.Length > 0) {
            var images = await this.imageService.List(merchantId, EItemImage.UserBackIdentityCard, model.Id!, false);
            await this.imageService.Save(merchantId, EItemImage.UserBackIdentityCard, model.Id!, model.BackIdentityCard, entity: images.FirstOrDefault());
        }

        if (model.BankCard?.FrontBankCard.Data != null && model.BankCard?.FrontBankCard.Data.Length > 0) {
            var images = await this.imageService.List(merchantId, EItemImage.UserBackIdentityCard, model.BankCard.Id!, false);
            await this.imageService.Save(merchantId, EItemImage.UserFontBankCard, model.BankCard.Id!, model.BankCard.FrontBankCard, entity: images.FirstOrDefault());
        }

        if (model.BankCard?.BackBankCard.Data != null && model.BankCard?.BackBankCard.Data.Length > 0) {
            var images = await this.imageService.List(merchantId, EItemImage.UserBackBankCard, model.BankCard.Id!, false);
            await this.imageService.Save(merchantId, EItemImage.UserBackBankCard, model.BankCard.Id!, model.BankCard.BackBankCard, entity: images.FirstOrDefault());
        }
        return contact.Id;
    }
}

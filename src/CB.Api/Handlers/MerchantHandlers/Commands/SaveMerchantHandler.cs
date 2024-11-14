using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Infrastructure.Services.Interfaces;
using MerchantDto = CB.Api.Models.MerchantDto;

namespace CB.Api.Handlers.MerchantHandlers.Commands;

public class SaveMerchantCommand : Common.ModelRequest<MerchantDto> { }

public class SaveMerchantHandler(IServiceProvider serviceProvider) : Common.BaseHandler<SaveMerchantCommand>(serviceProvider) {
    public readonly IImageService imageService = serviceProvider.GetRequiredService<IImageService>();

    public override async Task Handle(SaveMerchantCommand request, CancellationToken cancellationToken) {
        var model = request.Model;
        var merchantId = request.MerchantId;
        model.Name = model.Name.Trim();
        var entity = await db.Merchants.AsTracking().FirstOrDefaultAsync(o => o.Id == model.Id, cancellationToken);
        CbException.ThrowIfNull(entity, Messages.Merchant_NotFound);

        entity.Name = model.Name;
        entity.SearchName = StringHelper.UnsignedUnicode(model.Name);
        entity.Phone = model.Phone;
        entity.Email = model.Email;
        entity.Address = model.Address;
        entity.Commune = model.Commune?.Code;
        entity.District = model.District?.Code;
        entity.Province = model.Province?.Code;
        await db.SaveChangesAsync(cancellationToken);

        if (model.Logo.Data != null && model.Logo.Data.Length > 0) {
            var logos = await this.imageService.List(merchantId, EItemImage.MerchantLogo, model.Id!, false);
            await this.imageService.Save(merchantId, EItemImage.MerchantLogo, model.Id!, model.Logo, entity: logos.FirstOrDefault());
        }
    }
}

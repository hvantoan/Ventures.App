using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.MerchantHandlers.Commands;

public class SaveMerchantCommand : ModelRequest<MerchantDto> { }

public class SaveMerchantHandler(IServiceProvider serviceProvider) : BaseHandler<SaveMerchantCommand>(serviceProvider) {

    public override async Task Handle(SaveMerchantCommand request, CancellationToken cancellationToken) {
        var model = request.Model;
        var merchantId = request.MerchantId;
        model.Name = model.Name.Trim();
        var entity = await db.Merchants.FirstOrDefaultAsync(o => o.Id == model.Id, cancellationToken);
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
    }
}

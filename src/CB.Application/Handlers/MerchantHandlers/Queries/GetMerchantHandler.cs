using CB.Domain.Common.Resource;
using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.MerchantHandlers.Queries;

public class GetMerchantQuery : Request<MerchantDto> { }

public class GetMerchantHandler(IServiceProvider serviceProvider) : BaseHandler<GetMerchantQuery, MerchantDto>(serviceProvider) {
    private readonly UnitResource unitResource = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<MerchantDto> Handle(GetMerchantQuery request, CancellationToken cancellationToken) {
        var merchant = await db.Merchants.AsNoTracking().FirstOrDefaultAsync(o => o.Id == request.MerchantId, cancellationToken);
        CbException.ThrowIfNull(merchant, Messages.Merchant_NotFound);
        CbException.ThrowIfFalse(merchant.IsActive, Messages.Merchant_Inactive);
        CbException.ThrowIf(merchant.ExpiredDate <= DateTimeOffset.UtcNow, Messages.Merchant_Expired);

        return MerchantDto.FromEntity(merchant, unitResource, this.url);
    }
}

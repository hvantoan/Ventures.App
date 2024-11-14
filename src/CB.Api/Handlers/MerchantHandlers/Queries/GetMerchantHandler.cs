using CB.Domain.Common.Resource;
using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using MerchantDto = CB.Api.Models.MerchantDto;

namespace CB.Api.Handlers.MerchantHandlers.Queries;

public class GetMerchantQuery : Common.Request<MerchantDto> { }

public class GetMerchantHandler(IServiceProvider serviceProvider) : Common.BaseHandler<GetMerchantQuery, MerchantDto>(serviceProvider) {
    private readonly UnitResource unitResource = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<MerchantDto> Handle(GetMerchantQuery request, CancellationToken cancellationToken) {
        var merchant = await db.Merchants.AsNoTracking().FirstOrDefaultAsync(o => o.Id == request.MerchantId, cancellationToken);
        CbException.ThrowIfNull(merchant, Messages.Merchant_NotFound);
        CbException.ThrowIfFalse(merchant.IsActive, Messages.Merchant_Inactive);
        CbException.ThrowIf(merchant.ExpiredDate <= DateTimeOffset.UtcNow, Messages.Merchant_Expired);

        // Get images (logo)
        var typeInclued = new List<EItemImage> { EItemImage.MerchantLogo };
        var images = await this.db.ItemImages.AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && o.ItemId == merchant.Id)
            .Where(o => typeInclued.Contains(o.ItemType))
            .GroupBy(o => o.ItemType)
            .ToDictionaryAsync(o => o.Key, v => v.ToList(), cancellationToken);
        var logo = images.GetValueOrDefault(EItemImage.MerchantLogo)?.FirstOrDefault();

        return MerchantDto.FromEntity(merchant, unitResource, this.url, logo);
    }
}

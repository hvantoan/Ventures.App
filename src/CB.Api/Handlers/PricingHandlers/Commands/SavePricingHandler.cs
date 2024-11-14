using CB.Domain.Extentions;
using PricingDto = CB.Api.Models.PricingDto;

namespace CB.Api.Handlers.PricingHandlers.Commands;

public class SavePricingCommand : Common.ModelRequest<PricingDto, string> {
}

internal class SavePricingHandler(IServiceProvider serviceProvider) : Common.BaseHandler<SavePricingCommand, string>(serviceProvider) {

    public override async Task<string> Handle(SavePricingCommand request, CancellationToken cancellationToken) {
        var model = request.Model;

        var pricing = await this.db.Pricings.AsTracking()
            .Include(o => o.Features)
            .FirstOrDefaultAsync(o => o.Id == model.Id, cancellationToken);

        if (pricing == null) {
            pricing = new Pricing {
                Id = model.Id,
                Price = model.Price,
                MonetaryUnit = model.MonetaryUnit,
                Interval = model.Interval,
                Features = model.Features.Select(o => new Feature {
                    Id = NGuidHelper.New(),
                    Content = o,
                }).ToList()
            };
            this.db.Pricings.Add(pricing);
        } else {
            pricing.Interval = model.Interval;
            pricing.MonetaryUnit = model.MonetaryUnit;
            pricing.Price = model.Price;
            pricing.Features = model.Features.Select(o => new Feature {
                Id = NGuidHelper.New(),
                Content = o
            }).ToList();
        }

        await this.db.SaveChangesAsync(cancellationToken);
        return pricing.Id;
    }
}

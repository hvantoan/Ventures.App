using PricingDto = CB.Api.Models.PricingDto;

namespace CB.Api.Handlers.PricingHandlers.Queries;

public class ListPricingQuery : IRequest<List<PricingDto>> { }


internal class ListPricingHandler(IServiceProvider serviceProvider) : Common.BaseHandler<ListPricingQuery, List<PricingDto>>(serviceProvider) {

    public override async Task<List<PricingDto>> Handle(ListPricingQuery request, CancellationToken cancellationToken) {
        var pricings = await this.db.Pricings.Include(o => o.Features)
            .Select(p => new PricingDto {
                Id = p.Id,
                Price = p.Price,
                MonetaryUnit = p.MonetaryUnit,
                Interval = p.Interval,
                Features = p.Features!.Select(f => f.Content).ToList()
            }).ToListAsync(cancellationToken);

        return pricings;
    }
}

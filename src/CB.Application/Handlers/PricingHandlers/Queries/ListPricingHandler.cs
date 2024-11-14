using Models_PricingDto = CB.Application.Models.PricingDto;
using PricingDto = CB.Application.Models.PricingDto;

namespace CB.Application.Handlers.PricingHandlers.Queries;

public class ListPricingQuery : IRequest<List<Models_PricingDto>> { }


internal class ListPricingHandler(IServiceProvider serviceProvider) : Common.BaseHandler<ListPricingQuery, List<Models_PricingDto>>(serviceProvider) {

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

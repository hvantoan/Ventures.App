using CB.Domain.Extentions;
using CB.Infrastructure.Services.Interfaces;

namespace CB.Application.Handlers.BotHandlers.Queries;

public class ListBotQuery : PaginatedRequest<ListBot> { }

public class ListBot : PaginatedList<BotDto> { }

internal class ListBotHandler(IServiceProvider serviceProvider) : BaseHandler<ListBotQuery, ListBot>(serviceProvider) {
    private readonly IImageService imageService = serviceProvider.GetRequiredService<IImageService>();

    public override async Task<ListBot> Handle(ListBotQuery request, CancellationToken cancellationToken) {
        var query = this.db.Bots.WhereIf(!string.IsNullOrEmpty(request.SearchText), o => o.SearchName.Contains(request.SearchText!));
        var count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken);
        var bots = await query.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken);
        var botIds = bots.Select(o => o.Id).ToList();

        var avatars = await this.db.ItemImages.Where(o => botIds.Contains(o.ItemId) && o.ItemType == Domain.Enums.EItemImage.BotAvatar)
              .GroupBy(o => o.ItemId).ToDictionaryAsync(k => k.Key, v => v.FirstOrDefault(), cancellationToken);


        return new ListBot {
            Count = count,
            Items = bots.Select(o => BotDto.FromEntity(o, url, avatars.GetValueOrDefault(o.Id))).ToList(),
        };
    }
}

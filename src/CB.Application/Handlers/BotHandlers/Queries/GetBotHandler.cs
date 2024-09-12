using CB.Domain.Constants;
using CB.Domain.Extentions;
using CB.Infrastructure.Services.Interfaces;

namespace CB.Application.Handlers.BotHandlers.Queries;

public class GetBotQuery : SingleRequest<BotDto> { }

internal class GetBotHandler(IServiceProvider serviceProvider) : BaseHandler<GetBotQuery, BotDto>(serviceProvider) {
    private readonly IImageService imageService = serviceProvider.GetRequiredService<IImageService>();

    public override async Task<BotDto> Handle(GetBotQuery request, CancellationToken cancellationToken) {
        var bot = await this.db.Bots.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == request.Id, cancellationToken);
        CbException.ThrowIfNull(bot, Messages.Bot_NotFound);

        var avatar = await imageService.List(request.MerchantId, Domain.Enums.EItemImage.BotAvatar, bot.Id);
        return BotDto.FromEntity(bot, url, avatar.FirstOrDefault());
    }
}

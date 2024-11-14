using CB.Domain.Common.Resource;
using CB.Domain.Constants;
using CB.Domain.Extentions;
using UserBotDto = CB.Api.Models.UserBotDto;

namespace CB.Api.Handlers.UserBotHandlers.Queries;

public class GetUserBotQuery : Common.SingleRequest<UserBotDto> { }

internal class GetUserBotHandler(IServiceProvider serviceProvider) : Common.BaseHandler<GetUserBotQuery, UserBotDto>(serviceProvider) {
    private readonly UnitResource unitRes = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<UserBotDto> Handle(GetUserBotQuery request, CancellationToken cancellationToken) {
        var userBot = await this.db.UserBots.Include(o => o.User).Include(o => o.Bot)
            .FirstOrDefaultAsync(o => o.Id == request.Id && o.MerchantId == request.MerchantId, cancellationToken);
        CbException.ThrowIfNull(userBot, Messages.UserBot_NotFound);

        return UserBotDto.FromEntity(userBot, userBot.Bot, userBot.User, unitRes);

    }
}

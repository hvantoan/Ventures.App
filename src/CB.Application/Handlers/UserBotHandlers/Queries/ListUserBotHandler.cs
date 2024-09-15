using CB.Domain.Common.Resource;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.UserBotHandlers.Queries;

public class ListUserBotQuery : PaginatedRequest<ListUserBot> {
}

public class ListUserBot : PaginatedList<UserBotDto> { }

internal class ListUserBotHandler(IServiceProvider serviceProvider) : BaseHandler<ListUserBotQuery, ListUserBot>(serviceProvider) {
    private readonly UnitResource unitRes = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<ListUserBot> Handle(ListUserBotQuery request, CancellationToken cancellationToken) {
        var query = this.db.UserBots
            .Include(o => o.User).Include(o => o.Bot)
            .Where(o => o.MerchantId == request.MerchantId)
            .WhereIf(!string.IsNullOrEmpty(request.SearchText), o =>
                        o.Bot!.SearchName.Contains(request.SearchText!)
                    || (!string.IsNullOrEmpty(o.BrokerServer) && o.BrokerServer.Contains(request.SearchText!))
                    || o.ID_MT4.ToString().Contains(request.SearchText!) || o.User!.SearchName.Contains(request.SearchText!)
            );

        return new ListUserBot {
            Count = await query.CountAsync(cancellationToken),
            Items = await query.Skip(request.Skip).Take(request.Take).Select(o => UserBotDto.FromEntity(o, o.Bot, o.User, unitRes)).ToListAsync(cancellationToken)
        };
    }
}

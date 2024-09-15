using CB.Domain.Common.Resource;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.TransactionHandlers.Queries;

public class ListTransactionQuery : PaginatedRequest<ListTransaction> {
    public string UserBotId { get; set; } = string.Empty;
}

public class ListTransaction : PaginatedList<TransactionDto> { }

internal class ListTransactionHandler(IServiceProvider serviceProvider) : BaseHandler<ListTransactionQuery, ListTransaction>(serviceProvider) {
    private readonly UnitResource unitRes = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<ListTransaction> Handle(ListTransactionQuery request, CancellationToken cancellationToken) {
        var query = this.db.Transactions
            .Include(o => o.UserBot!).ThenInclude(o => o.User)
            .Include(o => o.UserBot!).ThenInclude(o => o.Bot)
            .Where(o => o.MerchantId == request.MerchantId)
            .WhereIf(!string.IsNullOrEmpty(request.SearchText), o =>
                        o.UserBot!.BrokerServer!.Contains(request.SearchText!)
                    || o.UserBot.ID_MT4.ToString().Contains(request.SearchText!)
                    || o.UserBot.Bot!.Name.Contains(request.SearchText!)
            )
            .WhereIf(!string.IsNullOrWhiteSpace(request.UserBotId), o => o.UserBotId == request.UserBotId)
            .OrderByDescending(o => o.TransactionAt);

        return new ListTransaction {
            Count = await query.CountAsync(cancellationToken),
            Items = await query
                .WhereFunc(!request.IsAll, q => q.Skip(request.Skip).Take(request.Take))
                .Select(o => TransactionDto.FromEntity(o, o.UserBot, unitRes)).ToListAsync(cancellationToken)
        };
    }
}

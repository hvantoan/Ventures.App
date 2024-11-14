using CB.Domain.Constants;
using CB.Domain.Extentions;
using SingleRequest = CB.Api.Common.SingleRequest;

namespace CB.Api.Handlers.TransactionHandlers.Commands;

public class CalculateBalanceCommand : SingleRequest {
}

internal class CalculateBalanceHandler(IServiceProvider serviceProvider) : Common.BaseHandler<CalculateBalanceCommand>(serviceProvider) {

    public override async Task Handle(CalculateBalanceCommand request, CancellationToken cancellationToken) {
        var userBot = await this.db.UserBots.AsTracking()
             .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        CbException.ThrowIfNull(userBot, Messages.UserBot_NotFound);
        userBot.Balance = await this.db.Transactions
               .Where(o => o.UserBotId == request.Id)
               .SumAsync(o => o.TransactionType == Domain.Enums.ETransactionType.Withdrawal ? -o.Amount : o.Amount, cancellationToken);

        await this.db.SaveChangesAsync(cancellationToken);
    }
}

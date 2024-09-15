using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.TransactionHandlers.Commands;

public class SaveTransactionCommand : ModelRequest<TransactionDto, string> {
}

internal class SaveTransactionHandler(IServiceProvider serviceProvider) : BaseHandler<SaveTransactionCommand, string>(serviceProvider) {
    private readonly IMediator mediator = serviceProvider.GetRequiredService<IMediator>();
    public override async Task<string> Handle(SaveTransactionCommand request, CancellationToken cancellationToken) {

        var userId = request.UserId;
        var merchantId = request.MerchantId;
        var model = request.Model;

        await Validate(model);

        return string.IsNullOrWhiteSpace(model.Id)
           ? await Create(merchantId, userId!, model, cancellationToken)
           : await Update(merchantId, userId!, model, cancellationToken);
    }

    private async Task Validate(TransactionDto model) {
        var botExits = await this.db.UserBots.AnyAsync(bot => bot.Id == model.UserBotId && !bot.IsDelete);
        CbException.ThrowIf(!botExits, Messages.UserBot_NotFound);
        var userExits = await this.db.Users.AnyAsync(o => o.Id == model.Id && !o.IsDelete && !o.IsSystem);
        CbException.ThrowIf(!botExits, Messages.UserBot_UserRequired);

        if (model.TransactionType == ETransactionType.Outcome) {
            var userBot = await this.db.UserBots.FirstOrDefaultAsync(o => o.Id == model.UserBotId && !o.IsDelete);
            CbException.ThrowIf(userBot!.Balance - model.Amount < 0, Messages.Transaction_AmountNotRatherThanBalance);
        }

    }

    private async Task<string> Create(string merchantId, string userId, TransactionDto model, CancellationToken cancellationToken) {

        var transaction = new Transaction() {
            Id = NGuidHelper.New(model.Id),
            Amount = model.Amount,
            TransactionAt = DateTimeOffset.Now,
            TransactionType = model.TransactionType,
            UserBotId = model.UserBotId,
            MerchantId = merchantId,
        };

        await db.Transactions.AddAsync(transaction, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        await this.mediator.Send(new CalculateBalanceCommand { Id = model.UserBotId }, cancellationToken);

        return transaction.Id;
    }

    private async Task<string> Update(string merchantId, string userId, TransactionDto model, CancellationToken cancellationToken) {
        var entity = await db.Transactions.AsTracking().FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);
        CbException.ThrowIfNull(entity, Messages.UserBot_NotFound);
        entity.Amount = model.Amount;
        await db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

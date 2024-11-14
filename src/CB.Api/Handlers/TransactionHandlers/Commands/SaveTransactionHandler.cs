using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using TransactionDto = CB.Api.Models.TransactionDto;

namespace CB.Api.Handlers.TransactionHandlers.Commands;

public class SaveTransactionCommand : Common.ModelRequest<TransactionDto, string> {
}

internal class SaveTransactionHandler(IServiceProvider serviceProvider) : Common.BaseHandler<SaveTransactionCommand, string>(serviceProvider) {
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

        if (model.TransactionType == ETransactionType.Withdrawal) {
            var userBot = await this.db.UserBots.FirstOrDefaultAsync(o => o.Id == model.UserBotId && !o.IsDelete);
            CbException.ThrowIf(userBot!.Balance - model.Amount < 0, Messages.Transaction_AmountNotRatherThanBalance);
        }
    }

    private async Task<string> Create(string merchantId, string userId, TransactionDto model, CancellationToken cancellationToken) {
        var server = await this.db.UserBots.AsTracking()
            .FirstOrDefaultAsync(o => o.MerchantId == merchantId && o.Id == model.UserBotId, cancellationToken);
        var transaction = new Transaction() {
            Id = NGuidHelper.New(model.Id),
            BeforeBalance = server!.Balance,
            Amount = model.Amount,
            AfterBalance = server.Balance - model.Amount,
            TransactionAt = DateTimeOffset.Now,
            TransactionType = model.TransactionType,
            UserBotId = model.UserBotId,
            MerchantId = merchantId,
        };

        decimal monneyChange = model.TransactionType switch {
            ETransactionType.Deposit => model.Amount,
            ETransactionType.Withdrawal => -model.Amount,
            _ => 0
        };

        server.Balance = server.Balance + monneyChange;

        await db.Transactions.AddAsync(transaction, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
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

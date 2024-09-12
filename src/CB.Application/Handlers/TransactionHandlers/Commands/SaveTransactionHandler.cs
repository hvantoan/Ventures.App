using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.TransactionHandlers.Commands;

public class SaveTransactionCommand : ModelRequest<TransactionDto, string> {
}

internal class SaveTransactionHandler(IServiceProvider serviceProvider) : BaseHandler<SaveTransactionCommand, string>(serviceProvider) {

    public override async Task<string> Handle(SaveTransactionCommand request, CancellationToken cancellationToken) {

        var userId = request.UserId;
        var merchantId = request.MerchantId;
        var model = request.Model;

        await Validate(model!);

        return string.IsNullOrWhiteSpace(model.Id)
           ? await Create(merchantId, userId!, model, cancellationToken)
           : await Update(merchantId, userId!, model, cancellationToken);
    }

    private async Task Validate(TransactionDto model) {
        var botExits = await this.db.UserBots.AnyAsync(bot => bot.Id == model.UserBotId);
        CbException.ThrowIf(!botExits, Messages.UserBot_NotFound);
        var userExits = await this.db.Users.AnyAsync(o => o.Id == model.Id && !o.IsDelete && !o.IsSystem);
        CbException.ThrowIf(!botExits, Messages.UserBot_UserRequired);
    }

    private async Task<string> Create(string merchantId, string userId, TransactionDto model, CancellationToken cancellationToken) {
        var userBot = new Transaction() {
            Id = NGuidHelper.New(model.Id),
            Amount = model.Amount,
            TransactionDate = model.TransactionDate,
            TransactionType = model.TransactionType,
            UserBotId = model.UserBotId,
        };

        await db.Transactions.AddAsync(userBot, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return userBot.Id;
    }

    private async Task<string> Update(string merchantId, string userId, TransactionDto model, CancellationToken cancellationToken) {
        var entity = await db.Transactions.AsTracking().FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);
        CbException.ThrowIfNull(entity, Messages.UserBot_NotFound);
        entity.Amount = model.Amount;
        await db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

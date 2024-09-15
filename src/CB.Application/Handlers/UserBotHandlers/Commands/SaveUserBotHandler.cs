using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.UserBotHandlers.Commands;

public class SaveUserBotCommand : ModelRequest<UserBotDto, string> { }

internal class SaveUserBotHandler(IServiceProvider serviceProvider) : BaseHandler<SaveUserBotCommand, string>(serviceProvider) {

    public override async Task<string> Handle(SaveUserBotCommand request, CancellationToken cancellationToken) {
        var userId = request.UserId;
        var merchantId = request.MerchantId;
        var model = request.Model;

        await Validate(model!);

        return string.IsNullOrWhiteSpace(model.Id)
           ? await Create(merchantId, userId!, model, cancellationToken)
           : await Update(merchantId, userId!, model, cancellationToken);
    }

    private async Task Validate(UserBotDto model) {
        var botExits = await this.db.Bots.AnyAsync(bot => bot.Id == model.Bot!.Id && !bot.IsDelete);
        CbException.ThrowIf(!botExits, Messages.UserBot_BotRequired);
        var userExits = await this.db.Users.AnyAsync(o => o.Id == model.User!.Id && !o.IsDelete && !o.IsSystem);
        CbException.ThrowIf(!botExits, Messages.UserBot_UserRequired);
        CbException.ThrowIf(model.ID_MT4 <= 0, Messages.UserBot_IdMT4Required);
    }

    private async Task<string> Create(string merchantId, string userId, UserBotDto model, CancellationToken cancellationToken) {
        var userBot = new UserBot() {
            Id = NGuidHelper.New(model.Id),
            UserId = userId,
            Balance = model.Balance,
            BotId = model.Bot!.Id,
            MerchantId = merchantId,
            BrokerServer = model.BrokerServer,
            EV = model.EV,
            ID_MT4 = model.ID_MT4,
            PassView = model.PassView,
            PassWeb = model.PassWeb,
            Ref = model.Ref,
            CreatAt = DateTimeOffset.Now,
        };

        if (userBot.Balance > 0) {
            userBot.Transactions.Add(new Transaction {
                Id = NGuidHelper.New(),
                UserBotId = userBot.Id,
                MerchantId = merchantId
                TransactionAt = DateTimeOffset.Now,
                Amount = userBot.Balance,
                TransactionType = ETransactionType.Income,
            });
        }

        await db.UserBots.AddAsync(userBot, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return userBot.Id;
    }

    private async Task<string> Update(string merchantId, string userId, UserBotDto model, CancellationToken cancellationToken) {
        var userBot = await db.UserBots.AsTracking().FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId, cancellationToken);
        CbException.ThrowIfNull(userBot, Messages.UserBot_NotFound);

        userBot.ID_MT4 = model.ID_MT4;
        userBot.BrokerServer = model.BrokerServer;
        userBot.EV = model.EV;
        userBot.Ref = model.Ref;
        userBot.PassView = model.PassView;
        userBot.PassWeb = model.PassWeb;

        await db.SaveChangesAsync(cancellationToken);
        return userBot.Id;
    }
}

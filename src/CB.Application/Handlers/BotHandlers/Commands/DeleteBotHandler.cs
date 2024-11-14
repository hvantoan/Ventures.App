using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.BotHandlers.Commands;

public class DeleteBotCommand : Common.SingleRequest<string> { }

internal class DeleteBotHandler(IServiceProvider serviceProvider) : Common.BaseHandler<DeleteBotCommand, string>(serviceProvider) {

    public override async Task<string> Handle(DeleteBotCommand request, CancellationToken cancellationToken) {
        var bot = await this.db.Bots.AsTracking()
            .FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete, cancellationToken);
        CbException.ThrowIfNull(bot, Messages.Bot_NotFound);
        bot.IsDelete = true;
        await this.db.SaveChangesAsync(cancellationToken);

        return bot.Id;
    }
}

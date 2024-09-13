using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Infrastructure.Services.Interfaces;

namespace CB.Application.Handlers.BotHandlers.Commands;

public class SaveBotCommand : ModelRequest<BotDto, string> {
}

internal class SaveBotHandler(IServiceProvider serviceProvider) : BaseHandler<SaveBotCommand, string>(serviceProvider) {
    private readonly IImageService imageService = serviceProvider.GetRequiredService<IImageService>();

    public override async Task<string> Handle(SaveBotCommand request, CancellationToken cancellationToken) {
        var model = request.Model;
        using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        try {
            var bot = await this.db.Bots.AsTracking().FirstOrDefaultAsync(o => o.Id == model.Id, cancellationToken);
            if (bot == null) {
                bot = new Bot {
                    Id = NGuidHelper.New(),
                    MerchantId = request.MerchantId,
                    Name = model.Name,
                    SearchName = StringHelper.UnsignedUnicode(model.Name),
                    Description = model.Description,
                    CreatedAt = DateTimeOffset.Now,
                };
                await this.db.Bots.AddAsync(bot, cancellationToken);
            } else {
                bot.Name = model.Name;
                bot.Description = model.Description;
                bot.SearchName = StringHelper.UnsignedUnicode(model.Name);
            }

            await this.db.SaveChangesAsync(cancellationToken);

            if (model.Avatar.Data != null && model.Avatar.Data.Length > 0) {
                var avatar = await this.imageService.List(request.MerchantId, EItemImage.BotAvatar, bot.Id!, true);
                await this.imageService.Save(request.MerchantId, EItemImage.BotAvatar, bot.Id!, model.Avatar, entity: avatar.FirstOrDefault());
            }

            await transaction.CommitAsync(cancellationToken);
            return bot.Id;
        } catch (Exception ex) {
            await transaction.RollbackAsync(cancellationToken);
            throw new Exception(ex.Message);
        }
    }
}

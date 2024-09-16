using CB.Domain.Common.Resource;
using CB.Domain.Enums;

namespace CB.Application.Models;

public class TransactionDto {
    public string Id { get; set; } = string.Empty;
    public string UserBotId { get; set; } = null!;
    public string MerchantId { get; set; } = null!;
    public decimal Amount { get; set; }
    public ETransactionType TransactionType { get; set; }
    public DateTimeOffset TransactionAt { get; set; }
    public UserBotDto? UserBot { get; set; }

    public static TransactionDto FromEntity(Transaction entity, UserBot? userBot = null, UnitResource? unitRes = null) {
        return new TransactionDto {
            Id = entity.Id,
            UserBotId = entity.UserBotId,
            MerchantId = entity.MerchantId,
            Amount = entity.Amount,
            TransactionAt = entity.TransactionAt,
            TransactionType = entity.TransactionType,
            UserBot = userBot == null ? null : UserBotDto.FromEntity(userBot, userBot.Bot, userBot.User, unitRes),
        };
    }
}

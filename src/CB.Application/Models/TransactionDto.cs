using CB.Domain.Common.Resource;
using CB.Domain.Enums;

namespace CB.Application.Models;

public class TransactionDto {
    public string Id { get; set; } = null!;
    public string UserBotId { get; set; } = null!;
    public decimal Amount { get; set; }
    public ETransactionType TransactionType { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public UserBotDto? UserBot { get; set; }

    public static TransactionDto FromEntity(Transaction entity, UserBot? userBot = null, UnitResource? unitRes = null) {
        return new TransactionDto {
            Id = entity.Id,
            Amount = entity.Amount,
            TransactionDate = entity.TransactionAt,
            TransactionType = entity.TransactionType,
            UserBotId = entity.UserBotId,
            UserBot = userBot == null ? null : UserBotDto.FromEntity(userBot, userBot.Bot, userBot.User, unitRes),
        };
    }
}

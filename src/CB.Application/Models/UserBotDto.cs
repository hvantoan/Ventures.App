using CB.Domain.Common.Resource;

namespace CB.Application.Models;

public class UserBotDto {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string BotId { get; set; } = string.Empty;
    public string? BrokerServer { get; set; }
    public long ID_MT4 { get; set; }
    public string? PassView { get; set; }
    public string? PassWeb { get; set; }
    public decimal Balance { get; set; }
    public long EV { get; set; }
    public long Ref { get; set; }

    public DateTimeOffset CreateAt { get; set; }

    public virtual UserDto? User { get; set; }
    public virtual BotDto? Bot { get; set; }

    public List<TransactionDto>? Transactions { get; set; }

    public static UserBotDto FromEntity(UserBot entity, Bot? bot = null, User? user = null, UnitResource? unitRes = null) {
        return new UserBotDto {
            Id = entity.Id,
            UserId = entity.UserId,
            BotId = entity.BotId,
            BrokerServer = entity.BrokerServer,
            PassView = entity.PassView,
            PassWeb = entity.PassWeb,
            Balance = entity.Balance,
            ID_MT4 = entity.ID_MT4,
            EV = entity.EV,
            Ref = entity.Ref,
            CreateAt = entity.CreatAt,
            Bot = bot == null ? null : BotDto.FromEntity(bot),
            User = user == null ? null : UserDto.FromEntity(user, unitRes),
        };
    }
}

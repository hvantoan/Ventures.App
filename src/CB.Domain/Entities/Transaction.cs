using CB.Domain.Common.Interfaces;
using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class Transaction : IEntity {
    public string Id { get; set; } = null!;
    public string UserBotId { get; set; } = null!;
    public decimal Amount { get; set; }
    public ETransactionType TransactionType { get; set; }
    public DateTimeOffset TransactionAt { get; set; }
    public virtual UserBot? UserBot { get; set; }
}

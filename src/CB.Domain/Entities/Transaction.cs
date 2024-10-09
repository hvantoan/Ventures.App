using CB.Domain.Common.Interfaces;
using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class Transaction : IEntity {
    public required string Id { get; set; }
    public required string UserBotId { get; set; }
    public required string MerchantId { get; set; }

    public decimal BeforeBalance { get; set; }
    public decimal Amount { get; set; }
    public decimal AfterBalance { get; set; }
    public ETransactionType TransactionType { get; set; }
    public DateTimeOffset TransactionAt { get; set; }
    public virtual UserBot? UserBot { get; set; }
}

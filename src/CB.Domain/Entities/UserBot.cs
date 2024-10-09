using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class UserBot : IEntity {
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required string BotId { get; set; }
    public required string MerchantId { get; set; }
    public string? BrokerServer { get; set; }
    public long IdMt4 { get; set; }
    public string? PassView { get; set; }
    public string? PassWeb { get; set; }
    public decimal Balance { get; set; }
    public long Ev { get; set; }
    public long Ref { get; set; }

    public DateTimeOffset CreateAt { get; set; }
    public bool IsDelete { get; set; }

    public virtual User? User { get; set; }
    public virtual Bot? Bot { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = [];

    public virtual ICollection<ServerReport> ServerReports { get; set; } = [];
}

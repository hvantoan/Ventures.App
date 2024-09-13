using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public partial class UserBot : IEntity {
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string BotId { get; set; } = null!;
    public string? BrokerServer { get; set; }
    public long ID_MT4 { get; set; }
    public string? PassView { get; set; }
    public string? PassWeb { get; set; }
    public decimal Balance { get; set; }
    public long EV { get; set; }
    public long Ref { get; set; }


    public DateTimeOffset CreatAt { get; set; }
    public bool IsDelete { get; set; }

    public virtual User? User { get; set; }
    public virtual Bot? Bot { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = [];
}


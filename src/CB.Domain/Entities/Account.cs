namespace CB.Domain.Entities;

public class Account {
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string? BrokerServer { get; set; }
    public long ID_MT4 { get; set; }
    public string? PassView { get; set; }
    public string? PassWeb { get; set; }
    public decimal Balance { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<AccountBot>? AccountBots { get; set; }
    public virtual ICollection<Transaction>? Transactions { get; set; }
}

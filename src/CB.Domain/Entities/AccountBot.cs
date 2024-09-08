namespace CB.Domain.Entities;

public class AccountBot {
    public string AccountId { get; set; } = null!;
    public string BotId { get; set; } = null!;
    public long EV { get; set; }
    public long Ref { get; set; }

    public virtual Account? Account { get; set; }
    public virtual Bot? Bot { get; set; }
}

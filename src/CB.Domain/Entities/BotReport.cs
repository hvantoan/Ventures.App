using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class BotReport : IEntity {
    public string Id { get; set; } = null!;
    public string BotId { get; set; } = null!;
    public string MerchantId { get; set; } = null!;

    public int Month { get; set; }
    public int Year { get; set; }
    public decimal Balance { get; set; }
    public decimal Profit { get; set; }
    public decimal ProfitPercent { get; set; }

    public virtual Bot? Bot { get; set; }
}

using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class BotReport : IEntity {
    public required string Id { get; set; }
    public required string BotId { get; set; }
    public required string MerchantId { get; set; }

    public int Month { get; set; }
    public int Year { get; set; }
    public decimal Balance { get; set; }
    public decimal Profit { get; set; }
    public decimal ProfitPercent { get; set; }

    public virtual Bot? Bot { get; set; }
}

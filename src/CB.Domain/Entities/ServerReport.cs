using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class ServerReport : IEntity {
    public string Id { get; set; } = null!;
    public string UserBotId { get; set; } = null!;
    public string MerchantId { get; set; } = null!;

    public int Month { get; set; }
    public int Year { get; set; }

    public decimal BeforeBalance { get; set; }
    public decimal Deposit { get; set; }
    public decimal AffterBalance { get; set; }

    public decimal Profit { get; set; }
    public decimal ProfitPercent { get; set; }
    public decimal ProfitActual { get; set; }

    public decimal BeforeAsset { get; set; }
    public decimal Withdrawal { get; set; }
    public decimal AffterAsset { get; set; }

    public int Commission { get; set; }

    public virtual UserBot? UserBot { get; set; }
}

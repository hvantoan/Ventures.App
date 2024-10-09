using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class ServerReport : IEntity {
    public required string Id { get; set; }
    public required string UserBotId { get; set; }
    public required string MerchantId { get; set; }

    public int Month { get; set; }
    public int Year { get; set; }

    public decimal BeforeBalance { get; set; }
    public decimal Deposit { get; set; }
    public decimal AfterBalance { get; set; }

    public decimal Profit { get; set; }
    public decimal ProfitPercent { get; set; }
    public decimal ProfitActual { get; set; }

    public decimal BeforeAsset { get; set; }
    public decimal Withdrawal { get; set; }
    public decimal AfterAsset { get; set; }

    public int Commission { get; set; }

    public virtual UserBot? UserBot { get; set; }
}

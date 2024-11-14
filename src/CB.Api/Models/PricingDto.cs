using CB.Domain.Enums;

namespace CB.Api.Models;

public class PricingDto {
    public string Id { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string MonetaryUnit { get; set; } = string.Empty;
    public EPricingInterval Interval { get; set; }
    public List<string> Features { get; set; } = [];
}

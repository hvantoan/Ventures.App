using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class Pricing {
    public required string Id { get; set; }
    public decimal Price { get; set; }
    public required string MonetaryUnit { get; set; }
    public EPricingInterval Interval { get; set; }
    public ICollection<Feature>? Features { get; set; }
}


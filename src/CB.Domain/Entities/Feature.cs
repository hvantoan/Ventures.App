namespace CB.Domain.Entities;

public class Feature {
    public required string Id { get; set; }
    public required string Content { get; set; }
    public string PricingId { get; set; } = null!;
    public Pricing? Pricing { get; set; }
}

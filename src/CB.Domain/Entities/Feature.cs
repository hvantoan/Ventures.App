namespace CB.Domain.Entities;

public class Feature {
    public required string Id { get; set; }
    public required string Content { get; set; }
    public Guid PricingId { get; set; }
    public Pricing? Pricing { get; set; }
}

namespace CB.Domain.Entities;

public class Feature {
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;

    public Guid PricingId { get; set; }
    public Pricing? Pricing { get; set; }
}

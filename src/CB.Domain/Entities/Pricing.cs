namespace CB.Domain.Entities;

public class Pricing {
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public string MonetaryUnit { get; set; } = null!;
    public EPricingInterval Interval { get; set; }
    public ICollection<Feature>? Features { get; set; }
}

public enum EPricingInterval {
    Monthly,
    Yearly
}

public enum EPricingType {
    Free,
    Basic,
    Premium,
    Enterprise
}

namespace CB.Application.Models;

public class PricingDto {
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public string MonetaryUnit { get; set; } = null!;
    public EPricingInterval Interval { get; set; }
    public List<string> Features { get; set; } = [];
}

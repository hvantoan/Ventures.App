using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class Landing {
    public string Id { get; set; } = null!;
    public required string MerchantId { get; set; }
    public ELanding Type { get; set; }
    public required string Value { get; set; }
}

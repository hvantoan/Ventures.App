using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class Landing {
    public string Id { get; set; } = null!;
    public string MerchantId { get; set; } = null!;
    public ELanding Type { get; set; }
    public string Value { get; set; } = null!;
}

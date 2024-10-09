namespace CB.Domain.Entities;

public class Merchant {
    public required string Id { get; set; }

    public required string Code { get; set; }

    public required string Name { get; set; }

    public string SearchName { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? Commune { get; set; }

    public string? Address { get; set; }

    public bool IsActive { get; set; }
    public DateTimeOffset ExpiredDate { get; set; } = DateTimeOffset.UtcNow;
    public string? ApiSecret { get; set; }
    public long? At { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
}

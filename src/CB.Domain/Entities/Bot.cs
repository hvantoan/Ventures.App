using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class Bot : IEntity {
    public string Id { get; set; } = null!;
    public string MerchantId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string SearchName { get; set; } = null!;
    public string? Description { get; set; }

    public bool IsDelete { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public virtual ICollection<UserBot>? UserBots { get; set; }
}

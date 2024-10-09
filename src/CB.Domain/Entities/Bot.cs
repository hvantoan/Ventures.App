using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class Bot : IEntity {
    public required string Id { get; set; }
    public required string MerchantId { get; set; }
    public required string Name { get; set; }
    public required string SearchName { get; set; }
    public string? Description { get; set; }

    public bool IsDelete { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public virtual ICollection<UserBot>? UserBots { get; set; }
    public virtual ICollection<BotReport>? BotReports { get; set; }
}

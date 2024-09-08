using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public class Bot : IEntity {
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<AccountBot>? AccountBots { get; set; }
}

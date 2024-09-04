using CB.Domain.Common.Interfaces;
using CB.Domain.Enums;

namespace CB.Domain.Entities;

public partial class ItemImage : IEntity {
    public string Id { get; set; } = null!;
    public string MerchantId { get; set; } = null!;
    public string ItemId { get; set; } = null!;
    public EItemImage ItemType { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}

using CB.Domain.Common.Interfaces;
using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class ItemImage : IEntity {
    public required string Id { get; set; }
    public required string MerchantId { get; set; }
    public required string ItemId { get; set; }
    public EItemImage ItemType { get; set; }
    public required string Name { get; set; }
    public required string Image { get; set; }
}

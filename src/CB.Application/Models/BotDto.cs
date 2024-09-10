using CB.Domain.ExternalServices.Models;

namespace CB.Application.Models;

public class BotDto {
    public string Id { get; set; } = string.Empty;
    public string MerchantId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public ImageDto Avatar { get; set; } = new();

    public static BotDto FromEntity(Bot entity, string? url = null, ItemImage? avatar = null) {
        return new BotDto {
            Id = entity.Id,
            MerchantId = entity.MerchantId,
            Name = entity.Name,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            Avatar = ImageDto.FromEntity(avatar, url) ?? new ImageDto()
        };
    }
}

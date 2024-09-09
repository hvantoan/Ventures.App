using CB.Domain.Entities;
using CB.Domain.Enums;
using CB.Domain.ExternalServices.Models;

namespace CB.Infrastructure.Services.Interfaces;

public interface IImageService {

    Task<List<ItemImage>> List(string merchantId, EItemImage itemType, string itemId, bool withNoTracking = true);

    Task<List<ItemImage>> List(string merchantId, EItemImage itemType, List<string> itemIds, bool withNoTracking = true);

    Task Save(string merchantId, EItemImage itemType, string itemId, ImageDto model, ItemImage? entity = null);

    Task Delete(string id, ItemImage? entity);
}

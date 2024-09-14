using CB.Domain.Common.Helpers;
using CB.Domain.Constants;
using CB.Domain.Entities;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Domain.ExternalServices.Models;
using CB.Infrastructure.Database;
using CB.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Infrastructure.Services.Implements;

public class ImageService(IServiceProvider serviceProvider) : IImageService {
    private readonly IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
    private readonly CBContext db = serviceProvider.GetRequiredService<CBContext>();

    public async Task<List<ItemImage>> List(string merchantId, EItemImage itemType, string itemId, bool withNoTracking = true) {
        return await this.List(merchantId, itemType, [itemId], withNoTracking);
    }

    public async Task<List<ItemImage>> List(string merchantId, EItemImage itemType, List<string> itemIds, bool withNoTracking = true) {
        var query = this.db.ItemImages.Where(o => o.MerchantId == merchantId && o.ItemType == itemType && itemIds.Contains(o.ItemId));
        if (!withNoTracking) query = query.AsTracking();
        return await query.ToListAsync();
    }

    public async Task Save(string merchantId, EItemImage itemType, string itemId, ImageDto? model, ItemImage? entity = null) {
        if (model == null || model.Data == null || model.Data.Length == 0)
            return;

        if (!string.IsNullOrWhiteSpace(model.Id)) {
            await this.Delete(model.Id, null);
        }
        await Create(merchantId, itemType, itemId, model);
    }

    public async Task Delete(string id, ItemImage? entity) {
        entity ??= await this.db.ItemImages.AsTracking().FirstOrDefaultAsync(o => o.Id == id);
        CbException.ThrowIfNull(entity, Messages.Image_Error);

        await FtpHelper.DeleteFileAsync(entity.Image, configuration);
        this.db.ItemImages.Remove(entity);
        await this.db.SaveChangesAsync();
    }

    private async Task Create(string merchantId, EItemImage itemType, string itemId, ImageDto model) {
        ItemImage entity = new() {
            Id = NGuidHelper.New(),
            MerchantId = merchantId,
            ItemId = itemId,
            ItemType = itemType,
            Name = model.Name,
        };
        entity.Image = await this.UploadImageAsync(entity, model.Data);

        await this.db.AddAsync(entity);
        await this.db.SaveChangesAsync();
    }

    private async Task<string> UploadImageAsync(ItemImage item, byte[]? data) {
        ArgumentNullException.ThrowIfNull(data);

        string fileType = EFile.Images.ToString().ToLower();
        string extentions = Path.GetExtension(item.Name);

        var filename = $"{fileType}/{item.Id}{extentions}";
        await FtpHelper.UploadImageAsync(filename, data, configuration);

        return filename;
    }
}

using System.Diagnostics.CodeAnalysis;
using CB.Domain.Common.Resource;
using CB.Domain.ExternalServices.Models;


namespace CB.Application.Models;

public class MerchantDto {
    public string Id { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public Domain.Common.Resource.Unit? Province { get; set; }
    public Domain.Common.Resource.Unit? District { get; set; }
    public Domain.Common.Resource.Unit? Commune { get; set; }

    public List<ImageDto> Banners { get; set; } = new();
    public ImageDto Logo { get; set; } = new();
    public ImageDto Icon { get; set; } = new();
    public ImageDto ReceiptLogo { get; set; } = new();

    [return: NotNullIfNotNull(nameof(entity))]
    public static MerchantDto? FromEntity(Merchant? entity, UnitResource? unitResource = null, string? url = null,
        List<ItemImage>? banners = null, ItemImage? logo = null,
        ItemImage? icon = null, ItemImage? receiptLogo = null) {
        if (entity == null) return default;
        return new MerchantDto {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Email = entity.Email,
            Phone = entity.Phone,
            Address = entity.Address,
            Commune = unitResource?.GetByCode(entity.Commune),
            District = unitResource?.GetByCode(entity.District),
            Province = unitResource?.GetByCode(entity.Province),
            Banners = banners?.Select(o => ImageDto.FromEntity(o, url)).ToList() ?? [],
            Logo = ImageDto.FromEntity(logo, url) ?? new ImageDto(),
            Icon = ImageDto.FromEntity(icon, url) ?? new ImageDto(),
            ReceiptLogo = ImageDto.FromEntity(receiptLogo, url) ?? new ImageDto(),
        };
    }

    public string GetFullAddress(bool isUseShortName = false) {
        var list = new List<string>();
        if (!string.IsNullOrWhiteSpace(Address))
            list.Add(Address);
        if (Commune != null)
            list.Add(isUseShortName ? Commune.ShortName : Commune.Name);
        if (District != null)
            list.Add(isUseShortName ? District.ShortName : District.Name);
        if (Province != null)
            list.Add(isUseShortName ? Province.ShortName : Province.Name);
        return string.Join(", ", list);
    }
}

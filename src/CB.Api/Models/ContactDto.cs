using CB.Domain.Extentions;
using CB.Domain.ExternalServices.Models;

namespace CB.Api.Models;

public class ContactDto {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string IdentityCard { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public BankCardDto? BankCard { get; set; }

    public ImageDto FrontIdentityCard { get; set; } = new();
    public ImageDto BackIdentityCard { get; set; } = new();

    public static ContactDto FromEntity(
            Contact entity, BankCard? bankCard = null, string? url = null,
            ItemImage? fontBank = null, ItemImage? backBank = null,
            ItemImage? fontIdCard = null, ItemImage? backIdCard = null
        ) {
        return new ContactDto {
            Id = entity.Id,
            UserId = entity.UserId,
            Name = entity.Name,
            Email = entity.Email,
            Phone = entity.Phone,
            IdentityCard = entity.IdentityCard,
            Address = entity.Address,
            CreateAt = entity.CreateAt,
            BankCard = bankCard != null ? BankCardDto.FromEntity(bankCard, url, fontBank, backBank) : null,
            FrontIdentityCard = ImageDto.FromEntity(fontIdCard, url) ?? new ImageDto(),
            BackIdentityCard = ImageDto.FromEntity(backIdCard, url) ?? new ImageDto()
        };
    }

    public Contact ToEntity() {
        return new Contact {
            Id = NGuidHelper.New(Id),
            UserId = UserId,
            Name = Name,
            SearchName = StringHelper.UnsignedUnicode(Name),
            Email = Email,
            Phone = Phone,
            IdentityCard = IdentityCard,
            Address = Address,
            CreateAt = CreateAt,
            BankCardId = string.Empty,
            BankCard = BankCard?.ToEntity(UserId),
        };
    }
}

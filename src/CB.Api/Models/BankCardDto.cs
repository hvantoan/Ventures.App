using CB.Domain.Extentions;
using CB.Domain.ExternalServices.Models;

namespace CB.Api.Models;

public class BankCardDto {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string? CardBranch { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Cvv { get; set; }
    public string ExpirationDate { get; set; } = string.Empty;

    public ImageDto FrontBankCard { get; set; } = new();
    public ImageDto BackBankCard { get; set; } = new();

    public static BankCardDto FromEntity(BankCard entity, string? url = null, ItemImage? font = null, ItemImage? back = null) {
        return new BankCardDto {
            Id = entity.Id,
            UserId = entity.UserId,
            CardNumber = entity.CardNumber,
            CardBranch = entity.CardBranch,
            Name = entity.Name,
            Cvv = entity.Cvv,
            ExpirationDate = entity.ExpirationDate,
            FrontBankCard = ImageDto.FromEntity(font, url) ?? new ImageDto(),
            BackBankCard = ImageDto.FromEntity(back, url) ?? new ImageDto(),
        };
    }

    public BankCard ToEntity(string? userId) {
        return new BankCard {
            Id = NGuidHelper.New(Id),
            UserId = !string.IsNullOrEmpty(userId) ? userId : UserId,
            CardNumber = CardNumber,
            CardBranch = CardBranch,
            Name = Name,
            Cvv = Cvv,
            ExpirationDate = ExpirationDate
        };
    }
}

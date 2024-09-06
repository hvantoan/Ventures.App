using CB.Domain.Extentions;

namespace CB.Application.Models;

public class BankCardDto {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string? CardBranch { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Cvv { get; set; }
    public string ExpirationDate { get; set; } = string.Empty;

    public static BankCardDto FromEntity(BankCard entity) {
        return new BankCardDto {
            Id = entity.Id,
            UserId = entity.UserId,
            CardNumber = entity.CardNumber,
            CardBranch = entity.CardBranch,
            Name = entity.Name,
            Cvv = entity.Cvv,
            ExpirationDate = entity.ExpirationDate
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

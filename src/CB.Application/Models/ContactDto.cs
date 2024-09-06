using CB.Domain.Extentions;

namespace CB.Application.Models;
public class ContactDto {
    public string Id { get; set; } = null!;
    public string? UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime CreateDate { get; set; }

    public BankCardDto? BankCard { get; set; }

    public static ContactDto FromEntity(Contact entity, BankCard? bankCard = null) {
        return new ContactDto {
            Id = entity.Id,
            UserId = entity.UserId,
            Name = entity.Name,
            Email = entity.Email,
            Phone = entity.Phone,
            Address = entity.Address,
            CreateDate = entity.CreateDate,
            BankCard = bankCard != null ? BankCardDto.FromEntity(bankCard) : null,
        };
    }

    public Contact ToEntity() {
        return new Contact {
            Id = NGuidHelper.New(Id),
            UserId = UserId,
            Name = Name,
            Email = Email,
            Phone = Phone,
            Address = Address,
            CreateDate = CreateDate,
            BankCard = BankCard?.ToEntity(),
        };
    }
}

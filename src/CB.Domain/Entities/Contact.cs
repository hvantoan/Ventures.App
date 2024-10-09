using CB.Domain.Common.Interfaces;
using CB.Domain.Extentions;

namespace CB.Domain.Entities;

public partial class Contact : IEntity {
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required string BankCardId { get; set; }
    public required string IdentityCard { get; set; }
    public required string Name { get; set; }
    public required string SearchName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public bool IsDelete { get; set; }

    public virtual User? User { get; set; }
    public virtual BankCard? BankCard { get; set; }
}

public partial class Contact {

    public void FromUpdate(Contact model) {
        this.Id = model.Id;
        this.Name = model.Name;
        this.SearchName = StringHelper.UnsignedUnicode(model.Name);
        this.Email = model.Email;
        this.Phone = model.Phone;
        this.Address = model.Address;
        this.BankCardId = model.BankCard!.Id!;
        this.BankCard?.FromUpdate(model.BankCard!);
    }
}

using CB.Domain.Common.Interfaces;
using CB.Domain.Extentions;

namespace CB.Domain.Entities;

public partial class Contact : IEntity {
    public string Id { get; set; } = null!;
    public string? UserId { get; set; }
    public string BankCardId { get; set; } = null!;
    public string IdentityCard { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string SearchName { get; set; } = null!;
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
        this.BankCard?.FromUpdate(model.BankCard!);
    }
}

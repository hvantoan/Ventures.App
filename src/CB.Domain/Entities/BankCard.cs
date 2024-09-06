using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public partial class BankCard : IEntity {
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public string? CardBranch { get; set; }
    public string Name { get; set; } = null!;
    public string? Cvv { get; set; }
    public string ExpirationDate { get; set; } = null!;
    public DateTimeOffset CreatedDate { get; set; }
    public bool IsDelete { get; set; }

    public virtual User? User { get; set; }
}

public partial class BankCard {

    public void FromUpdate(BankCard model) {
        this.CardNumber = model.CardNumber;
        this.CardBranch = model.CardBranch;
        this.Name = model.Name;
        this.Cvv = model.Cvv;
        this.ExpirationDate = model.ExpirationDate;
        this.IsDelete = model.IsDelete;
    }
}

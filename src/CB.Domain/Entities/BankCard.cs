using CB.Domain.Common.Interfaces;

namespace CB.Domain.Entities;

public partial class BankCard : IEntity {
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required string CardNumber { get; set; }
    public string? CardBranch { get; set; }
    public required string Name { get; set; }
    public string? Cvv { get; set; }
    public required string ExpirationDate { get; set; }
    public bool IsDelete { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
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

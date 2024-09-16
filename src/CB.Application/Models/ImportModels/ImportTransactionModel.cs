using FluentValidation;

namespace CB.Application.Models.ImportModels;

public class ImportTransactionModel : ImportModel {
    public DateTimeOffset TransactionDate { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string BrokerSever { get; set; } = null!;
    public long IDMT4 { get; set; }
    public string PassView { get; set; } = null!;
    public string PassWeb { get; set; } = null!;
    public decimal Banlance { get; set; }
    public string Bot { get; set; } = null!;
    public long Ev { get; set; }
    public long Ref { get; set; }
}

public class TransactionValidator : AbstractValidator<ImportTransactionModel> {

    public TransactionValidator() {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Tên thương hiệu không được để trống")
            .MaximumLength(255).WithMessage("Tên thương hiệu dài không quá 255 ký tự");

        RuleFor(o => o.Email)
            .EmailAddress().WithMessage("Sai định dạng email")
            .Unless(o => string.IsNullOrWhiteSpace(o.Email));
    }
}

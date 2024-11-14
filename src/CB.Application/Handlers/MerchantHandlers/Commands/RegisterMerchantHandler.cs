using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.MerchantHandlers.Commands;

public class RegisterMerchantCommand : IRequest {
    public RegisterMerchant Merchant { get; set; } = new();
    public RegisterAdminUser User { get; set; } = new();
}

public class RegisterMerchant {
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTimeOffset ExpiredDate { get; set; }
}

public class RegisterAdminUser {
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PinCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public Domain.Common.Resource.Unit? Province { get; set; }
    public Domain.Common.Resource.Unit? District { get; set; }
    public Domain.Common.Resource.Unit? Commune { get; set; }
    public string? Address { get; set; }
}

public class RegisterMerchantHandler(IServiceProvider serviceProvider) : Common.BaseHandler<RegisterMerchantCommand>(serviceProvider) {

    public override async Task Handle(RegisterMerchantCommand request, CancellationToken cancellationToken) {
        bool isThrow = string.IsNullOrWhiteSpace(request.Merchant?.Code)
            || string.IsNullOrWhiteSpace(request.Merchant?.Name)
            || string.IsNullOrWhiteSpace(request.User?.Username)
            || string.IsNullOrWhiteSpace(request.User?.Password)
            || string.IsNullOrWhiteSpace(request.User?.Name);
        CbException.ThrowIf(isThrow, Messages.Request_Invalid);

        var existed = await db.Merchants.AnyAsync(o => o.Code == request.Merchant!.Code, cancellationToken);
        CbException.ThrowIf(existed, Messages.Merchant_Existed);

        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var merchant = await InsertMerchant(request.Merchant!, cancellationToken);
        await InsertAdminUser(merchant, request.User!, cancellationToken);

        await InsertRole(merchant, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private async Task<Merchant> InsertMerchant(RegisterMerchant info, CancellationToken cancellationToken) {
        var merchant = new Merchant {
            Id = NGuidHelper.New(),
            Code = info.Code.Trim().ToLower(),
            Name = info.Name.Trim(),
            SearchName = StringHelper.UnsignedUnicode(info.Name),
            IsActive = true,
            ExpiredDate = info.ExpiredDate <= DateTimeOffset.Now
                ? DateTimeOffset.UtcNow.AddMonths(1)
                : info.ExpiredDate,
        };
        await db.Merchants.AddAsync(merchant, cancellationToken);
        return merchant;
    }

    private async Task InsertAdminUser(Merchant merchant, RegisterAdminUser info, CancellationToken cancellationToken) {
        var user = new User {
            Id = NGuidHelper.New(),
            MerchantId = merchant.Id,
            Username = info.Username,
            Password = PasswordHasher.Hash(info.Password),
            Name = info.Name,
            SearchName = StringHelper.UnsignedUnicode(info.Name),
            Phone = info.Phone,
            Province = info.Province?.Code,
            District = info.District?.Code,
            Commune = info.Commune?.Code,
            Address = info.Address,
            IsActive = true,
            IsAdmin = true,
        };
        await db.Users.AddAsync(user, cancellationToken);
    }

    private async Task InsertRole(Merchant merchant, CancellationToken cancellation) {
        var role = new Role {
            Id = NGuidHelper.New(),
            MerchantId = merchant.Id,
            Code = "RO_NguoiDung",
            Name = "Người dùng",
            IsClient = true,
            CreatedDate = DateTimeOffset.UtcNow,
            SearchName = StringHelper.UnsignedUnicode("Người dùng"),
        };
        await db.Roles.AddAsync(role, cancellation);
    }
}

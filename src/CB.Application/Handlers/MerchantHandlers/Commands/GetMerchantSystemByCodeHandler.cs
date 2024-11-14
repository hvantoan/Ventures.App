using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.MerchantHandlers.Commands;

public class GetMerchantSystemByCodeCommand : IRequest<MerchantSystemData> {
    public string MerchantCode { get; set; } = string.Empty;
}

public class MerchantSystemData {
    public string MerchantId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

public class GetMerchantSystemByCodeHandler(IServiceProvider serviceProvider) : Common.BaseHandler<GetMerchantSystemByCodeCommand, MerchantSystemData>(serviceProvider) {
    public override async Task<MerchantSystemData> Handle(GetMerchantSystemByCodeCommand request, CancellationToken cancellationToken) {
        var merchant = await db.Merchants.FirstOrDefaultAsync(o => o.Code == request.MerchantCode, cancellationToken);
        CbException.ThrowIfNull(merchant, Messages.Merchant_NotFound);
        CbException.ThrowIf(DateTimeOffset.Now > merchant.ExpiredDate, Messages.Merchant_Expired);

        var systemUser = await db.Users.AsNoTracking()
            .FirstOrDefaultAsync(o => o.MerchantId == merchant.Id && o.IsSystem, cancellationToken);
        if (systemUser == null) {
            systemUser = new User {
                Id = NGuidHelper.New(),
                MerchantId = merchant.Id,
                Username = "System",
                Password = "",
                Name = "System",
                SearchName = "System",
                IsSystem = true,
            };
            await db.Users.AddAsync(systemUser, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }

        return new MerchantSystemData {
            MerchantId = merchant.Id,
            UserId = systemUser.Id,
        };
    }
}

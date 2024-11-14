using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Extentions;
using Common_Request = CB.Application.Common.Request;

namespace CB.Application.Handlers.MeHandlers.Commands;

public class ChangePasswordMeCommand : Common_Request {
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

public class ChangePasswordMeHandler(IServiceProvider serviceProvider) : Common.BaseHandler<ChangePasswordMeCommand>(serviceProvider) {
    public override async Task Handle(ChangePasswordMeCommand request, CancellationToken cancellationToken) {
        var user = await db.Users.Where(o => o.Id == request.UserId && o.MerchantId == request.MerchantId && !o.IsDelete && !o.IsSystem)
            .FirstOrDefaultAsync(cancellationToken);
        CbException.ThrowIf(user == null, Messages.User_NotFound);
        CbException.ThrowIf(!PasswordHasher.Verify(request.OldPassword, user.Password), Messages.User_IncorrectOldPassword);

        user.Password = PasswordHasher.Hash(request.NewPassword);
        await db.SaveChangesAsync(cancellationToken);
    }
}

using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Extentions;
using Common_Request = CB.Application.Common.Request;

namespace CB.Application.Handlers.UserHandlers.Commands;

public class ChangePasswordUserCommand : Common_Request {
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}

public class ChangePasswordUserHandler(IServiceProvider serviceProvider) : Common.BaseHandler<ChangePasswordUserCommand>(serviceProvider) {

    public override async Task Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken) {
        var user = await db.Users.FirstOrDefaultAsync(o => o.Id == request.UserId
            && o.MerchantId == request.MerchantId && !o.IsDelete && !o.IsSystem, cancellationToken);
        CbException.ThrowIf(user == null, Messages.User_NotFound);
        CbException.ThrowIf(!PasswordHasher.Verify(request.OldPassword, user.Password), Messages.User_IncorrectOldPassword);

        user.Password = PasswordHasher.Hash(request.NewPassword);

        await db.SaveChangesAsync(cancellationToken);
    }
}

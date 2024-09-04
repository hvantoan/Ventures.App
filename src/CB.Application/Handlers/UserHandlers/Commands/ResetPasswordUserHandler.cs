using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.UserHandlers.Commands;

public class ResetPasswordUserCommand : Request {
    public string Password { get; set; } = null!;
}

public class ResetPasswordUserHandler : BaseHandler<ResetPasswordUserCommand> {

    public ResetPasswordUserHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public override async Task Handle(ResetPasswordUserCommand request, CancellationToken cancellationToken) {
        var user = await db.Users.FirstOrDefaultAsync(o => o.Id == request.UserId && o.MerchantId == request.MerchantId && !o.IsDelete && !o.IsSystem, cancellationToken);
        CbException.ThrowIfNull(user, Messages.User_NotFound);

        user.Password = PasswordHasher.Hash(request.Password);
        await db.SaveChangesAsync(cancellationToken);
    }
}

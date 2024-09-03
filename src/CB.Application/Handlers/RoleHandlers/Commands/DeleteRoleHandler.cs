using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.RoleHandlers.Commands;

public class DeleteRoleCommand : SingleRequest { }

public class DeleteRoleHandler(IServiceProvider serviceProvider) : BaseHandler<DeleteRoleCommand>(serviceProvider) {

    public override async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken) {
        var role = await db.Roles.Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete)
            .FirstOrDefaultAsync(cancellationToken);
        CbException.ThrowIfNull(role, Messages.Role_NotFound);

        var existedUser = await db.Users.AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && o.RoleId == role.Id && !o.IsDelete)
            .AnyAsync(cancellationToken);
        CbException.ThrowIf(existedUser, Messages.Role_NotDeleted);

        role.IsDelete = true;
        await db.SaveChangesAsync(cancellationToken);
    }
}

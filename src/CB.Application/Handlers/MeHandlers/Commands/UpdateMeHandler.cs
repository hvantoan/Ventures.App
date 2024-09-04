using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.MeHandlers.Commands;

public class UpdateMeCommand : ModelRequest<UserDto> { }

public class UpdateMeHandler(IServiceProvider serviceProvider) : BaseHandler<UpdateMeCommand>(serviceProvider) {

    public override async Task Handle(UpdateMeCommand request, CancellationToken cancellationToken) {
        var model = request.Model;

        var user = await db.Users.FirstOrDefaultAsync(o => o.Id == request.UserId && !o.IsDelete && !o.IsSystem, cancellationToken);
        CbException.ThrowIf(user == null, Messages.User_NotFound);
        CbException.ThrowIf(user.IsAdmin && !model.IsActive, Messages.User_NotInactive);
        CbException.ThrowIf(string.IsNullOrWhiteSpace(model.Name), Messages.User_NameIsRequire);

        var originUser = user.Clone();

        user.Name = model.Name;
        user.SearchName = StringHelper.UnsignedUnicode(model.Name);
        user.Phone = model.Phone;
        user.Province = model.Province?.Code;
        user.District = model.District?.Code;
        user.Commune = model.Commune?.Code;
        user.Address = model.Address;

        await db.SaveChangesAsync(cancellationToken);
    }
}

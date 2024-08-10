﻿using CB.Domain.Constants;
using CB.Domain.Extentions;
using Microsoft.EntityFrameworkCore;

namespace CB.Application.Handlers.UserHandlers.Commands;

public class UpdateProfileCommand : ModelRequest<UserDto> {
}

internal class UpdateProfileHandler(IServiceProvider serviceProvider) : BaseHandler<UpdateProfileCommand>(serviceProvider) {

    public override async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken) {
        var model = request.Model;
        var user = await db.Users.FirstOrDefaultAsync(o => o.Id == model.Id && o.Username == model.Username && !o.IsDeleted && !o.IsSystem, cancellationToken);
        DrException.ThrowIfNull(user, Messages.User_NotFound);

        DrException.ThrowIf(user.IsAdmin && !model.IsActive, Messages.User_Inactive);
        DrException.ThrowIf(string.IsNullOrWhiteSpace(model.Name), Messages.User_NameIsRequire);

        //var originUser = user.Clone();
        user.Name = model.Name;
        user.Phone = model.Phone;
        user.Address = model.Address;

        await db.SaveChangesAsync(cancellationToken);
    }
}

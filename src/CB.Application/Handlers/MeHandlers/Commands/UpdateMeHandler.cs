﻿using CB.Domain.Constants;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Infrastructure.Services.Interfaces;

namespace CB.Application.Handlers.MeHandlers.Commands;

public class UpdateMeCommand : ModelRequest<UserDto> { }

public class UpdateMeHandler(IServiceProvider serviceProvider) : BaseHandler<UpdateMeCommand>(serviceProvider) {
    private readonly IImageService imageService = serviceProvider.GetRequiredService<IImageService>();

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

        if (model.Avatar.Data != null && model.Avatar.Data.Length > 0) {
            var avatars = await this.imageService.List(request.MerchantId, EItemImage.UserAvatar, model.Id!, false);
            await this.imageService.Save(request.MerchantId, EItemImage.UserAvatar, model.Id!, model.Avatar, entity: avatars.FirstOrDefault());
        }
    }
}

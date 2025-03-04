﻿using CB.Domain.Common.Resource;
using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.UserHandlers.Queries;

public class GetUserQuery : SingleRequest<UserDto?> { }

public class GetUserHandler(IServiceProvider serviceProvider) : BaseHandler<GetUserQuery, UserDto?>(serviceProvider) {
    private readonly UnitResource unitRes = serviceProvider.GetRequiredService<UnitResource>();

    public override async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken) {
        var user = await db.Users.AsNoTracking()
            .Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete && !o.IsSystem)
            .FirstOrDefaultAsync(cancellationToken);
        CbException.ThrowIfNull(user, Messages.User_NotFound);

        Role? role = null;
        if (!string.IsNullOrEmpty(user.RoleId)) {
            role = await db.Roles.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && o.Id == user.RoleId && !o.IsDelete)
                .FirstOrDefaultAsync(cancellationToken);
        }
        return UserDto.FromEntity(user, unitRes, role);
    }
}

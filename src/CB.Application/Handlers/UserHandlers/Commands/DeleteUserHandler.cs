﻿using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.UserHandlers.Commands;

public class DeleteUserCommand : SingleRequest { }

public class DeleteUserHandler : BaseHandler<DeleteUserCommand> {
    private readonly IMediator mediator;

    public DeleteUserHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    public override async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
        var entity = await db.Users.Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete && !o.IsSystem).FirstOrDefaultAsync(cancellationToken);
        CbException.ThrowIfNull(entity, Messages.User_NotFound);
        CbException.ThrowIf(entity.IsActive, Messages.User_NotDelete);

        entity.IsDelete = true;
        await db.SaveChangesAsync(cancellationToken);
    }
}

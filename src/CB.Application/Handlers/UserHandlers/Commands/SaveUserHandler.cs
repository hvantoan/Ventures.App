using CB.Domain.Common.Hashers;
using CB.Domain.Constants;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.UserHandlers.Commands;

public class SaveUserCommand : ModelRequest<UserDto, string> {
    public bool IsSelfUpdate { get; set; }
}

public class SaveUserHandler : BaseHandler<SaveUserCommand, string> {
    private readonly IMediator mediator;

    public SaveUserHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    public override async Task<string> Handle(SaveUserCommand request, CancellationToken cancellationToken) {
        var merchantId = request.MerchantId;
        var userId = request.UserId!;
        var model = request.Model;

        if (!request.IsSelfUpdate) {
            await Validate(merchantId, model.Role?.Id);
        }

        if (string.IsNullOrWhiteSpace(model.Id)) {
            model.Username = model.Username!.Trim().ToLower();
            return await Create(merchantId, userId, model, cancellationToken);
        }
        return await Update(merchantId, userId, model, request.IsSelfUpdate, cancellationToken);
    }

    private async Task<string> Create(string merchantId, string userId, UserDto model, CancellationToken cancellationToken) {
        var existed = await db.Users.AnyAsync(o => o.MerchantId == merchantId && o.Username == model.Username && !o.IsDelete && !o.IsSystem, cancellationToken);
        CbException.ThrowIf(existed, Messages.User_Existed);
        CbException.ThrowIf(string.IsNullOrWhiteSpace(model.Name), Messages.User_NameIsRequire);

        var user = new User() {
            Id = NGuidHelper.New(),
            MerchantId = merchantId,
            RoleId = model.Role!.Id,
            Username = model.Username!,
            Password = PasswordHasher.Hash(model.Password!),
            Name = model.Name,
            SearchName = StringHelper.UnsignedUnicode(model.Name),
            Phone = model.Phone,
            Province = model.Province?.Code,
            District = model.District?.Code,
            Commune = model.Commune?.Code,
            Address = model.Address,
            IsActive = model.IsActive,
            IsSystem = false,
            IsAdmin = false,
        };
        await db.Users.AddAsync(user, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    private async Task<string> Update(string merchantId, string userId, UserDto model, bool isSelfUpdate, CancellationToken cancellationToken) {
        var existed = await db.Users.AnyAsync(o => o.MerchantId == merchantId && o.Id != model.Id && o.Username == model.Username && !o.IsDelete && !o.IsSystem, cancellationToken);
        CbException.ThrowIf(existed, Messages.User_Existed);

        var user = await db.Users.AsTracking().FirstOrDefaultAsync(o => o.MerchantId == merchantId && o.Id == model.Id && o.Username == model.Username && !o.IsDelete && !o.IsSystem, cancellationToken);
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
        if (!isSelfUpdate) {
            user.RoleId = model.Role!.Id;
            user.IsActive = model.IsActive;
        }

        await db.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    private async Task Validate(string merchantId, string? roleId) {
        CbException.ThrowIf(string.IsNullOrWhiteSpace(roleId), Messages.Role_NotFound);

        var roleExisted = await db.Roles.AnyAsync(o => o.MerchantId == merchantId && o.Id == roleId && !o.IsDelete);
        CbException.ThrowIf(!roleExisted, Messages.Role_NotFound);
    }
}

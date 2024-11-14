using CB.Domain.Constants;
using CB.Domain.Extentions;
using Models_RoleDto = CB.Application.Models.RoleDto;
using PermissionDto = CB.Application.Models.PermissionDto;
using RoleDto = CB.Application.Models.RoleDto;

namespace CB.Application.Handlers.RoleHandlers.Commands;

public class SaveRoleCommand : Common.ModelRequest<Models_RoleDto, string> { }

public class SaveRoleHandler(IServiceProvider serviceProvider) : Common.BaseHandler<SaveRoleCommand, string>(serviceProvider) {
    private readonly IMediator mediator = serviceProvider.GetRequiredService<IMediator>();

    public override async Task<string> Handle(SaveRoleCommand request, CancellationToken cancellationToken) {
        var merchantId = request.MerchantId;
        var userId = request.UserId!;
        var model = request.Model;
        model.Code = model.Code?.Trim().ToUpper();
        model.Name = model.Name.Trim();

        model.Permissions = await ValidatePermissions(model.Permissions, cancellationToken);

        return string.IsNullOrWhiteSpace(model.Id)
            ? await Create(merchantId, userId, model, cancellationToken)
            : await Update(merchantId, userId, model, cancellationToken);
    }

    private async Task<List<PermissionDto>?> ValidatePermissions(List<PermissionDto>? permissions, CancellationToken cancellationToken) {
        if (permissions == null || !permissions.Any())
            return permissions;

        var activeItems = await db.GetPermissions(o => o.IsActive, cancellationToken);
        var activeItemIds = activeItems.Select(o => o.Id).ToList();
        return GetActivePermissions(permissions, activeItemIds);
    }

    private List<PermissionDto> GetActivePermissions(List<PermissionDto> permissions, List<string> activeItemIds, bool parentEnable = true) {
        permissions = permissions.Where(o => activeItemIds.Contains(o.Id)).ToList();

        foreach (var item in permissions) {
            item.IsEnable = parentEnable && item.IsEnable;
            item.Items = GetActivePermissions(item.Items, activeItemIds, item.IsEnable);
        }

        return permissions;
    }

    private async Task<string> Create(string merchantId, string userId, RoleDto model, CancellationToken cancellationToken) {
        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        CbException.ThrowIf(string.IsNullOrWhiteSpace(model.Code), Messages.Role_Existed);
        var existed = await db.Roles.AnyAsync(o => o.MerchantId == merchantId && o.Code == model.Code, cancellationToken);
        CbException.ThrowIf(existed, Messages.Role_Existed);

        Role entity = new() {
            Id = NGuidHelper.New(),
            MerchantId = merchantId,
            Code = model.Code,
            Name = model.Name,
            SearchName = StringHelper.UnsignedUnicode(model.Name),
        };
        entity.RolePermissions = GetRolePermissionEntities(model.Permissions, entity.Id);

        await db.Roles.AddAsync(entity, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return entity.Id;
    }

    private async Task<string> Update(string merchantId, string userId, RoleDto model, CancellationToken cancellationToken) {
        var existed = await db.Roles.AnyAsync(o => o.MerchantId == merchantId && o.Code == model.Code && o.Id != model.Id && !o.IsDelete, cancellationToken);
        CbException.ThrowIf(existed, Messages.Role_Existed);

        var role = await db.Roles.Include(o => o.RolePermissions).AsTracking()
            .FirstOrDefaultAsync(o => o.MerchantId == merchantId && o.Id == model.Id && !o.IsDelete, cancellationToken);
        CbException.ThrowIfNull(role, Messages.Role_NotFound);

        var roleOriginal = role.Clone();

        role.Name = model.Name;
        role.SearchName = StringHelper.UnsignedUnicode(model.Name);

        role.RolePermissions = this.GetRolePermissionEntities(model.Permissions, role.Id);

        await db.SaveChangesAsync(cancellationToken);
        return role.Id;
    }

    private List<RolePermission> GetRolePermissionEntities(List<PermissionDto>? permissions, string roleId) {
        List<RolePermission> rolePermissions = new();
        if (permissions != null && permissions.Any()) {
            foreach (var item in permissions) {
                rolePermissions.Add(new RolePermission {
                    RoleId = roleId,
                    PermissionId = item.Id,
                    IsEnable = item.IsEnable,
                });
                rolePermissions.AddRange(GetRolePermissionEntities(item.Items, roleId));
            }
        }
        return rolePermissions;
    }
}

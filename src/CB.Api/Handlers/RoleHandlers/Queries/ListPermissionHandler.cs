using BaseHandler = CB.Api.Common.BaseHandler;
using PermissionDto = CB.Api.Models.PermissionDto;

namespace CB.Api.Handlers.RoleHandlers.Queries;

public class AllPermissionQuery : IRequest<ListPermissionData> { }

public class ListPermissionData : Common.PaginatedList<PermissionDto> { }

public class ListPermissionHandler : BaseHandler, IRequestHandler<AllPermissionQuery, ListPermissionData> {

    public ListPermissionHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public async Task<ListPermissionData> Handle(AllPermissionQuery request, CancellationToken cancellationToken) {
        var permissions = await db.GetPermissions(o => o.IsActive, cancellationToken);
        return new() { Items = PermissionDto.FromEntities(permissions)! };
    }
}

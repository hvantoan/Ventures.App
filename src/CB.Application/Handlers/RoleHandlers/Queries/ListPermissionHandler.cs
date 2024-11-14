using Common_BaseHandler = CB.Application.Common.BaseHandler;
using Models_PermissionDto = CB.Application.Models.PermissionDto;
using PermissionDto = CB.Application.Models.PermissionDto;

namespace CB.Application.Handlers.RoleHandlers.Queries;

public class AllPermissionQuery : IRequest<ListPermissionData> { }

public class ListPermissionData : Common.PaginatedList<Models_PermissionDto> { }

public class ListPermissionHandler : Common_BaseHandler, IRequestHandler<AllPermissionQuery, ListPermissionData> {

    public ListPermissionHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public async Task<ListPermissionData> Handle(AllPermissionQuery request, CancellationToken cancellationToken) {
        var permissions = await db.GetPermissions(o => o.IsActive, cancellationToken);
        return new() { Items = PermissionDto.FromEntities(permissions)! };
    }
}

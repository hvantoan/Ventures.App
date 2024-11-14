using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using AllPermissionQuery = CB.Api.Handlers.RoleHandlers.Queries.AllPermissionQuery;
using AllRoleQuery = CB.Api.Handlers.RoleHandlers.Queries.AllRoleQuery;
using DeleteRoleCommand = CB.Api.Handlers.RoleHandlers.Commands.DeleteRoleCommand;
using GetRoleQuery = CB.Api.Handlers.RoleHandlers.Queries.GetRoleQuery;
using ListRoleQuery = CB.Api.Handlers.RoleHandlers.Queries.ListRoleQuery;
using RoleDto = CB.Api.Models.RoleDto;
using SaveRoleCommand = CB.Api.Handlers.RoleHandlers.Commands.SaveRoleCommand;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize(CbClaim.Web.Setting_Role, CbClaim.Web.Setting_User), Route("api/role")]
public class RoleController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpGet, CbAuthorize, Route("all")]
    public async Task<Result> All() {
        var data = await this.mediator.Send(new AllRoleQuery {
            MerchantId = this.merchantId,
            UserId = this.userId,
        });
        return Result.Ok(data);
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Setting_Role), Route("list")]
    public async Task<Result> List(ListRoleQuery req) {
        req.MerchantId = this.merchantId;
        req.UserId = this.userId;
        var data = await this.mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Setting_Role), Route("get")]
    public async Task<Result> Get(GetRoleQuery req) {
        req.MerchantId = this.merchantId;
        req.UserId = this.userId;
        var data = await this.mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpGet, CbAuthorize(CbClaim.Web.Setting_Role), Route("permission")]
    public async Task<Result> Permission() {
        var data = await this.mediator.Send(new AllPermissionQuery());
        return Result.Ok(data);
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Setting_Role), Route("save")]
    public async Task<Result> Save([FromBody] RoleDto model) {
        var itemId = await this.mediator.Send(new SaveRoleCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model,
        });
        return Result.Ok(itemId);
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Setting_Role), Route("delete")]
    public async Task<Result> Delete(DeleteRoleCommand req) {
        req.MerchantId = this.merchantId;
        req.UserId = this.userId;
        await this.mediator.Send(req);
        return Result.Ok();
    }
}

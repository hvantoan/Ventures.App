using CB.Application.Handlers.UserHandlers.Commands;
using CB.Application.Handlers.UserHandlers.Queries;
using CB.Application.Models;
using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CB.Application.Controllers;

[ApiController, CbAuthorize(CbClaim.Web.Setting_User), Route("api/user")]
public class UserController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpGet, Route("all")]
    public async Task<Result> All() {
        var data = await mediator.Send(new AllUserQuery {
            MerchantId = merchantId,
            UserId = userId,
        });
        return Result.Ok(data);
    }

    [HttpPost, Route("list")]
    public async Task<Result> List(ListUserQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpPost, Route("get")]
    public async Task<Result> Get(GetUserQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpPost, Route("save")]
    public async Task<Result> Save(UserDto model) {
        await mediator.Send(new SaveUserCommand {
            MerchantId = merchantId,
            UserId = userId,
            Model = model,
        });
        return Result.Ok();
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Setting_User_Reset), Route("reset-password")]
    public async Task<Result> ResetPassword(ResetPasswordUserCommand req) {
        req.MerchantId = merchantId;
        await mediator.Send(req);
        return Result.Ok();
    }

    [HttpPost, Route("delete")]
    public async Task<Result> Delete(DeleteUserCommand req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        await mediator.Send(req);
        return Result.Ok();
    }
}

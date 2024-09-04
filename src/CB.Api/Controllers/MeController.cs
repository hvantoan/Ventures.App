using CB.Application.Handlers.MeHandlers.Commands;
using CB.Application.Handlers.MeHandlers.Queries;
using CB.Application.Models;
using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize, Route("api/me")]
public class MeController : BaseController {

    public MeController(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    [HttpGet]
    public async Task<Result> GetMe() {
        var data = await this.mediator.Send(new GetMeQuery {
            MerchantId = merchantId,
            UserId = userId
        });
        return Result.Ok(data);
    }

    [HttpPost]
    public async Task<Result> UpdateMe(UserDto model) {
        var req = new UpdateMeCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model,
        };
        await this.mediator.Send(req);
        return Result.Ok();
    }

    [HttpPost, Route("change-password")]
    public async Task<Result> ChangePassword(ChangePasswordMeCommand req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        await this.mediator.Send(req);
        return Result.Ok();
    }



}

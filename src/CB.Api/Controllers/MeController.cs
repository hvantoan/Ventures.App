using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using ChangePasswordMeCommand = CB.Api.Handlers.MeHandlers.Commands.ChangePasswordMeCommand;
using GetMeQuery = CB.Api.Handlers.MeHandlers.Queries.GetMeQuery;
using UpdateMeCommand = CB.Api.Handlers.MeHandlers.Commands.UpdateMeCommand;
using UserDto = CB.Api.Models.UserDto;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize, Route("api/me")]
public class MeController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {
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

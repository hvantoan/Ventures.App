using CB.Application.Handlers.UserBotHandlers.Commands;
using CB.Application.Handlers.UserBotHandlers.Queries;
using CB.Application.Models;
using CB.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, Route("api/servers")]
public class UserBotController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("save")]
    public async Task<Result> Save(UserBotDto model) {
        var id = await this.mediator.Send(new SaveUserBotCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model,
        });
        return Result.Ok(id);
    }

    [HttpPost, Route("list")]
    public async Task<Result> List(ListUserBotQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpGet, Route("{id}")]
    public async Task<Result> Get([FromRoute] string id) {
        var data = await mediator.Send(new GetUserBotQuery {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Id = id
        });
        return Result.Ok(data);
    }
}

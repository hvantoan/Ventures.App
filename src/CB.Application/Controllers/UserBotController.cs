using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using GetUserBotQuery = CB.Application.Handlers.UserBotHandlers.Queries.GetUserBotQuery;
using ListUserBotQuery = CB.Application.Handlers.UserBotHandlers.Queries.ListUserBotQuery;
using SaveUserBotCommand = CB.Application.Handlers.UserBotHandlers.Commands.SaveUserBotCommand;
using UserBotDto = CB.Application.Models.UserBotDto;

namespace CB.Application.Controllers;

[ApiController, CbAuthorize(CbClaim.Web.Service_Server), Route("api/servers")]
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

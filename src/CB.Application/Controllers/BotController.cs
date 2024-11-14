using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using BotDto = CB.Application.Models.BotDto;
using DeleteBotCommand = CB.Application.Handlers.BotHandlers.Commands.DeleteBotCommand;
using GetBotQuery = CB.Application.Handlers.BotHandlers.Queries.GetBotQuery;
using ListBotQuery = CB.Application.Handlers.BotHandlers.Queries.ListBotQuery;
using SaveBotCommand = CB.Application.Handlers.BotHandlers.Commands.SaveBotCommand;

namespace CB.Application.Controllers;

[ApiController, CbAuthorize(CbClaim.Web.Category_Bot), Route("api/bots")]
public class BotController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("save")]
    public async Task<Result> Save(BotDto model) {
        var id = await this.mediator.Send(new SaveBotCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model,
        });
        return Result.Ok(id);
    }

    [HttpPost, Route("list")]
    public async Task<Result> List(ListBotQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpGet, Route("{id}")]
    public async Task<Result> Get([FromRoute] string id) {
        var data = await mediator.Send(new GetBotQuery {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Id = id
        });
        return Result.Ok(data);
    }

    [HttpDelete, Route("{id}")]
    public async Task<Result> Delete([FromRoute] string id) {
        var data = await mediator.Send(new DeleteBotCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Id = id
        });
        return Result.Ok(data);
    }
}

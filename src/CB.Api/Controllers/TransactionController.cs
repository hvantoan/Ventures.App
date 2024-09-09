using CB.Application.Handlers.ContactHandlers.Commands;
using CB.Application.Handlers.ContactHandlers.Queries;
using CB.Application.Handlers.TransactionHandlers.Commands;
using CB.Application.Models;
using CB.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, Route("api/transactions")]
public class TransactionController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("save")]
    public async Task<Result> Save(ContactDto model) {
        var id = await this.mediator.Send(new SaveContactCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model,
        });
        return Result.Ok(id);
    }

    [HttpPost, Route("list")]
    public async Task<Result> List(ListContactQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpGet, Route("{id}")]
    public async Task<Result> Get([FromRoute] string id) {
        var data = await mediator.Send(new GetContactCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Id = id
        });
        return Result.Ok(data);
    }

    [HttpPost, Route("import")]
    public async Task<Result> Import(IFormFile file) {
        await mediator.Send(new ImportTransactionCommand {
            File = file
        });
        return Result.Ok();
    }
}

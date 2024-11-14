using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using ImportTransactionCommand = CB.Application.Handlers.TransactionHandlers.Commands.ImportTransactionCommand;
using ListTransactionQuery = CB.Application.Handlers.TransactionHandlers.Queries.ListTransactionQuery;
using SaveTransactionCommand = CB.Application.Handlers.TransactionHandlers.Commands.SaveTransactionCommand;
using TransactionDto = CB.Application.Models.TransactionDto;

namespace CB.Application.Controllers;

[ApiController, CbAuthorize(CbClaim.Web.Service_Transaction), Route("api/transactions")]
public class TransactionController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("save")]
    public async Task<Result> Save(TransactionDto model) {
        var id = await this.mediator.Send(new SaveTransactionCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model,
        });
        return Result.Ok(id);
    }

    [HttpPost, Route("list")]
    public async Task<Result> List(ListTransactionQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }

    //[HttpGet, Route("{id}")]
    //public async Task<Result> Get([FromRoute] string id) {
    //    var data = await mediator.Send(new GetContactCommand {
    //        MerchantId = this.merchantId,
    //        UserId = this.userId,
    //        Id = id
    //    });
    //    return Result.Ok(data);
    //}

    [HttpPost, Route("import")]
    public async Task<Result> Import(IFormFile file) {
        await mediator.Send(new ImportTransactionCommand {
            File = file
        });
        return Result.Ok();
    }
}

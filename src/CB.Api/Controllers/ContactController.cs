using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using ContactDto = CB.Api.Models.ContactDto;
using GetContactCommand = CB.Api.Handlers.ContactHandlers.Queries.GetContactCommand;
using ListContactQuery = CB.Api.Handlers.ContactHandlers.Queries.ListContactQuery;
using SaveContactCommand = CB.Api.Handlers.ContactHandlers.Commands.SaveContactCommand;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize, Route("api/contacts")]
public class ContactController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("save")]
    public async Task<Result> Save(ContactDto model) {
        var id = await this.mediator.Send(new SaveContactCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model,
        });
        return Result.Ok(id);
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Contact), Route("list")]
    public async Task<Result> List(ListContactQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }

    [HttpGet, CbAuthorize(CbClaim.Web.Contact), Route("{id}")]
    public async Task<Result> Get([FromRoute] string id) {
        var data = await mediator.Send(new GetContactCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Id = id
        });
        return Result.Ok(data);
    }
}

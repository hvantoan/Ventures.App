using CB.Application.Handlers.ContactHandlers.Commands;
using CB.Application.Handlers.ContactHandlers.Queries;
using CB.Application.Models;
using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize, Route("api/contact")]
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
}

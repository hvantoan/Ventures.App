using CB.Application.Handlers.ContactHandlers.Commands;
using CB.Application.Handlers.UserHandlers.Queries;
using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize, Route("api/contact")]
public class ContactController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("save")]
    public async Task<Result> Save(SaveContactCommand req) {
        var id = await this.mediator.Send(req);
        return Result.Ok(id);
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Contact), Route("list")]
    public async Task<Result> List(ListUserQuery req) {
        req.MerchantId = merchantId;
        req.UserId = userId;
        var data = await mediator.Send(req);
        return Result.Ok(data);
    }
}

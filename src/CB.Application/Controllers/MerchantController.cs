using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GetMerchantQuery = CB.Application.Handlers.MerchantHandlers.Queries.GetMerchantQuery;
using MerchantDto = CB.Application.Models.MerchantDto;
using RegisterMerchantCommand = CB.Application.Handlers.MerchantHandlers.Commands.RegisterMerchantCommand;
using SaveMerchantCommand = CB.Application.Handlers.MerchantHandlers.Commands.SaveMerchantCommand;

namespace CB.Application.Controllers;

[ApiController, AllowAnonymous, Route("api/merchant")]
public class MerchantController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("register")]
    public async Task<Result> Register(RegisterMerchantCommand req) {
        await this.mediator.Send(req);
        return Result.Ok();
    }

    [HttpGet, CbAuthorize(CbClaim.Web.Setting_General_Api), Route("get")]
    public async Task<Result> Get() {
        var data = await this.mediator.Send(new GetMerchantQuery {
            MerchantId = this.merchantId,
            UserId = this.userId
        });
        return Result.Ok(data);
    }

    [HttpPost, CbAuthorize(CbClaim.Web.Setting_General_Api), Route("save")]
    public async Task<Result> Save(MerchantDto model) {
        await this.mediator.Send(new SaveMerchantCommand {
            MerchantId = this.merchantId,
            UserId = this.userId,
            Model = model
        });
        return Result.Ok();
    }
}

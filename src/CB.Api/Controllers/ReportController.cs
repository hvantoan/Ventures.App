using CB.Application.Handlers.ReportHandlers;
using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize(CbClaim.Web.Dashboard), Route("api/reports")]
public class ReportController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("bot")]
    public async Task<Result> BotReport([FromQuery] BotReportQuery param) {
        param.MerchantId = merchantId;
        param.UserId = userId;
        var reports = await this.mediator.Send(param);
        return Result.Ok(reports);
    }
}

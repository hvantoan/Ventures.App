using CB.Domain.Common;
using CB.Domain.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using BotReportQuery = CB.Api.Handlers.ReportHandlers.BotReportQuery;
using ServerReportQuery = CB.Api.Handlers.ReportHandlers.ServerReportQuery;

namespace CB.Api.Controllers;

[ApiController, CbAuthorize(CbClaim.Web.Dashboard), Route("api/reports")]
public class ReportController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("bot")]
    public async Task<Result> BotReport(BotReportQuery request) {
        request.MerchantId = merchantId;
        request.UserId = userId;
        var reports = await this.mediator.Send(request);
        return Result.Ok(reports);
    }

    [HttpPost, Route("server")]
    public async Task<Result> ServerReport(ServerReportQuery request) {
        request.MerchantId = merchantId;
        request.UserId = userId;
        var reports = await this.mediator.Send(request);
        return Result.Ok(reports);
    }
}

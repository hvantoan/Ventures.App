using CB.Domain.Common;
using CB.Domain.Common.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, AllowAnonymous, Route("api/resource")]
public class ResourceController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {
    private readonly UnitResource unitRes = serviceProvider.GetRequiredService<UnitResource>();

    [HttpGet, Route("administrative-unit")]
    public async Task<Result> Permission([FromQuery] string? lv1, [FromQuery] string? lv2) {
        var data = unitRes.List(lv1, lv2);
        await Task.CompletedTask;
        return Result.Ok(data);
    }
}

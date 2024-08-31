using CB.Application.Handlers.PricingHandlers.Commands;
using CB.Application.Handlers.PricingHandlers.Queries;
using CB.Application.Models;
using CB.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers {

    [ApiController, Authorize, Route("api")]
    public class PricingController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

        [HttpGet, Authorize, Route("pricings")]
        public async Task<Result> List() {
            var res = await mediator.Send(new ListPricingQuery());
            return Result.Ok(res);
        }

        [HttpPost, Authorize, Route("pricings")]
        public async Task<Result> Save(PricingDto model) {
            var res = await mediator.Send(new SavePricingCommand() {
                Model = model
            });
            return Result.Ok(res);
        }
    }
}

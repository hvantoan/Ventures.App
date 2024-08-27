using CB.Application.Handlers.AuthHandlers.Commands;
using CB.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers;

[ApiController, AllowAnonymous, Route("api/auth")]
public class AuthController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, Route("login")]
    public async Task<Result> Login(LoginCommand req) {
        var res = await mediator.Send(req);
        return Result<LoginResult>.Ok(res);
    }

    [HttpPost, Route("google")]
    public async Task<Result> Google(RegisterGoogleCommand req) {
        var res = await mediator.Send(req);
        return Result<LoginResult>.Ok(res);
    }
}

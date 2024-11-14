using CB.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoginCommand = CB.Application.Handlers.AuthHandlers.Commands.LoginCommand;
using RegisterGoogleCommand = CB.Application.Handlers.AuthHandlers.Commands.RegisterGoogleCommand;

namespace CB.Application.Controllers;

[ApiController, Authorize, Route("api/auth")]
public class AuthController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {

    [HttpPost, AllowAnonymous, Route("login")]
    public async Task<Result> Login(LoginCommand req) {
        var res = await mediator.Send(req);
        return Result.Ok(res);
    }

    [HttpPost, Authorize, Route("google")]
    public async Task<Result> Google(RegisterGoogleCommand req) {
        var res = await mediator.Send(req);
        return Result.Ok(res);
    }
}

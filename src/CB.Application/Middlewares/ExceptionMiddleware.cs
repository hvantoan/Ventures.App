using System.Net;
using CB.Domain.Common;
using CB.Domain.Extentions;
using Microsoft.AspNetCore.Http;

namespace CB.Application.Middlewares;

public class ExceptionMiddleware : IMiddleware {

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next(context);
        } catch (CbException ex) {
            await HandleException(context, (int)HttpStatusCode.OK, ex);
        } catch (Exception ex) {
            await HandleException(context, (int)HttpStatusCode.BadRequest, ex);
        }
    }

    private static async Task HandleException(HttpContext context, int statusCode, Exception exception) {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        Console.WriteLine(exception.Message);
        await context.Response.WriteAsync(Result.Fail(exception.Message).ToString());
    }
}

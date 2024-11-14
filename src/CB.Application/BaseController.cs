using CB.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CB.Application;

public abstract class BaseController : ControllerBase {
    protected readonly IMediator mediator;
    protected readonly IHttpContextAccessor httpContextAccessor;
    protected readonly HttpContext? httpContext;

    protected readonly string merchantId;
    protected readonly string userId;
    protected readonly TimeSpan timezone;

    protected BaseController(IServiceProvider serviceProvider) {
        this.mediator = serviceProvider.GetRequiredService<IMediator>();
        this.httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        this.httpContext = this.httpContextAccessor.HttpContext;

        this.merchantId = this.GetToken(Constants.TokenMerchantId);
        this.userId = this.GetToken(Constants.TokenUserId);

        var tzStr = GetHeader(Constants.HeaderTimezone);
        if (!TimeSpan.TryParse(tzStr, out timezone)) {
            timezone = TimeSpan.FromHours(7);
        }
    }

    protected FileContentResult File(Domain.Common.FileResult file) {
        return File(file.ByteArray, "application/octet-stream", file.FileName);
    }

    protected string GetToken(string key) {
        return this.httpContext?.User?.FindFirst(o => o.Type == key)?.Value ?? string.Empty;
    }

    protected string GetHeader(string key) {
        if (this.httpContext?.Request != null && this.httpContext.Request.Headers.TryGetValue(key, out var value)) {
            return value.ToString();
        } else {
            return string.Empty;
        }
    }
}

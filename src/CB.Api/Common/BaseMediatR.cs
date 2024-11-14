using CB.Infrastructure.Database;

namespace CB.Api.Common;

public abstract class BaseMediatR(IServiceProvider serviceProvider) {
    protected readonly CBContext db = serviceProvider.GetRequiredService<CBContext>();
    protected readonly IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
}

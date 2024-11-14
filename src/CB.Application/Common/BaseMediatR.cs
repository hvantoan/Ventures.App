using CB.Infrastructure.Database;

namespace CB.Application.Common;

public abstract class BaseMediatR(IServiceProvider serviceProvider) {
    protected readonly CBContext db = serviceProvider.GetRequiredService<CBContext>();
    protected readonly IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
}

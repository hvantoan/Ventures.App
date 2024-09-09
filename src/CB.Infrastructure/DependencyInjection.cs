using CB.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Infrastructure {

    public static class DependencyInjection {

        public static IServiceCollection AddCBContext(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<CBContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString(nameof(CBContext)));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }

        public static void AutoMigration(this IServiceProvider serviceProvider) {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CBContext>();
            dbContext.Database.Migrate();
        }
    }
}

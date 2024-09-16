using CB.Infrastructure.Database;
using CB.Infrastructure.Jobs;
using CB.Infrastructure.Services.Implements;
using CB.Infrastructure.Services.Interfaces;
using Coravel;
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

        public static IServiceCollection AddJobs(this IServiceCollection services, IConfiguration configuration) {
            services.AddScheduler();
            services.AddScoped<BotReportJob>();
            return services;
        }

        public static IServiceProvider UseJobs(this IServiceProvider services) {
            services.UseScheduler(scheduler => {
                scheduler.Schedule<BotReportJob>().Monthly().RunOnceAtStart();
            }).OnError(ex => Console.WriteLine("Scheduler ERROR {0}", ex));
            return services;
        }

        public static void AutoMigration(this IServiceProvider serviceProvider) {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CBContext>();
            dbContext.Database.Migrate();
        }

        public static IServiceCollection AddInternalService(this IServiceCollection services) {
            services.AddScoped<IImageService, ImageService>();
            return services;
        }
    }
}

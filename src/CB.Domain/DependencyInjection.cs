using CB.Domain.ExternalServices.Implements;
using CB.Domain.ExternalServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Domain {

    public static class DependencyInjection {

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<IRedisService, RedisService>();
            return services;
        }
    }
}

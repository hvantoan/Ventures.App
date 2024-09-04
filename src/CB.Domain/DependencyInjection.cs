﻿using CB.Domain.Common.Configs;
using CB.Domain.Common.Resource;
using CB.Domain.ExternalServices.Implements;
using CB.Domain.ExternalServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CB.Domain {

    public static class DependencyInjection {

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration) {
            var config = configuration.GetSection("Redis").Get<RedisConfig>();
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect($"{config?.Host}:{config?.Port},password={config?.Password}"));
            services.AddScoped<IRedisService, RedisService>();
            return services;
        }

        public static IServiceCollection AddResources(this IServiceCollection services) {
            services.AddSingleton<UnitResource>();
            return services;
        }
    }
}

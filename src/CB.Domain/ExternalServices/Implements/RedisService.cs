﻿using CB.Domain.ExternalServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CB.Domain.ExternalServices.Implements {

    public class RedisService(IServiceProvider serviceProvider) : IRedisService {
        private readonly IDatabase redisCache = serviceProvider.GetRequiredService<IConnectionMultiplexer>().GetDatabase();

        public async Task<T?> GetAsync<T>(string key) {
            var json = await redisCache.StringGetAsync(key);
            return json.HasValue ? JsonConvert.DeserializeObject<T>(json.ToString()) : default;
        }

        public async Task RemoveAsync(string key) {
            await redisCache.KeyDeleteAsync(key);
        }

        public async Task SetAsync(string key, object? data, TimeSpan? ttl = null) {
            var json = JsonConvert.SerializeObject(data);
            await redisCache.StringSetAsync(key, json);
            if (ttl.HasValue && ttl.Value > TimeSpan.Zero)
                await redisCache.KeyExpireAsync(key, ttl);
        }

        public bool TryGetValue<T>(string key, out T? result) {
            var existed = redisCache.KeyExistsAsync(key).Result;
            if (existed) {
                result = GetAsync<T>(key).Result;
                return true;
            }
            result = default;
            return false;
        }
    }
}

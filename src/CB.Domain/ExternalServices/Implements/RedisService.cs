using CB.Domain.ExternalServices.Interfaces;
using CB.Domain.ExternalServices.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CB.Domain.ExternalServices.Implements;

public class RedisService(IServiceProvider serviceProvider) : IRedisService {
    private readonly IConnectionMultiplexer connection = serviceProvider.GetRequiredService<IConnectionMultiplexer>();

    public async Task<RedisValue<T>> GetAsync<T>(string key) {
        var redisCache = this.connection.GetDatabase();

        var existed = await redisCache.KeyExistsAsync(key);
        if (!existed) return new RedisValue<T>();

        var result = await redisCache.StringGetWithExpiryAsync(key);
        return new RedisValue<T> {
            Value = result.Value.HasValue ? JsonConvert.DeserializeObject<T>(result.Value.ToString()) : default,
            Expiry = result.Expiry,
        };
    }

    public async Task RemoveAsync(string key) {
        var redisCache = this.connection.GetDatabase();
        await redisCache.KeyDeleteAsync(key);
    }

    public async Task SetAsync(string key, object? data, TimeSpan? ttl = null) {
        var redisCache = this.connection.GetDatabase();
        var json = JsonConvert.SerializeObject(data);
        await redisCache.StringSetAsync(key, json);
        if (ttl.HasValue && ttl.Value > TimeSpan.Zero)
            await redisCache.KeyExpireAsync(key, ttl);
    }
}

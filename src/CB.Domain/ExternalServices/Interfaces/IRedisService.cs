using CB.Domain.ExternalServices.Models;

namespace CB.Domain.ExternalServices.Interfaces;

public interface IRedisService {

    Task<RedisValue<T>> GetAsync<T>(string key);

    Task SetAsync(string key, object? data, TimeSpan? ttl = null);

    Task RemoveAsync(string key);
}

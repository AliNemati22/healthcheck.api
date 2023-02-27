using domain.Interfaces;
using infrastructure.Persistence.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace healthcheck.api.Application.Common;

public class RedisCache
{
    private readonly ICacheStorage _redisStorage;
    public RedisCache(ICacheStorage redisStorage)
    {
        _redisStorage = redisStorage;
    }
    public async Task<Dictionary<string, string>> GetCacheValue(string key)
    {
        RedisValue redisValue = await _redisStorage.StringGetAsync(key);
        Dictionary<string, string> result = new();
        if (redisValue.HasValue)
        {
            var dic = JsonSerializer.Deserialize<Dictionary<string, string>>(redisValue);
            if (dic != null)
            {
                result = dic;
            }
        }
        return result;
    }
}
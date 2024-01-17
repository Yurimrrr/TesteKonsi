using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using TesteKonsi.Application.Contracts.Repository.Cache;
using TesteKonsi.Infra.Services.Configurations;

namespace TesteKonsi.Infra.Services.Cache;

public class RedisRepository : ICacheRepository
{
    private readonly IDistributedCache _distributedCache;
    private readonly RedisConfigurations _redisConfigurations;

    public RedisRepository(IDistributedCache distributedCache, IOptions<RedisConfigurations> redisConfigurations)
    {
        _distributedCache = distributedCache;
        _redisConfigurations = redisConfigurations.Value;
    }
    public async Task<T> GetValue<T>(string key)
    {
        var _key = key.ToLower();
        var result = await _distributedCache.GetStringAsync(_key);

        if (result == null)
            return default(T);

        return JsonConvert.DeserializeObject<T>(result);
    }

    public async Task<IEnumerable<T>> GetCollection<T>(string collectionKey)
    {
        var _key = collectionKey.ToLower();
        var result = await _distributedCache.GetStringAsync(_key);
        if (result == null)
            return default;

        return JsonConvert.DeserializeObject<IEnumerable<T>>(result);
    }

    public async Task SetValue<T>(string key, T value, DistributedCacheEntryOptions options = null)
    {
        var _key = key.ToLower();
        var newValue = JsonConvert.SerializeObject(value);
        await _distributedCache.SetStringAsync(_key, newValue, options);
    }

    public async Task SetCollection<T>(string collectionKey, IEnumerable<T> collectionValues, DistributedCacheEntryOptions options = null)
    {
        var _key = collectionKey.ToLower();
        var newValue = JsonConvert.SerializeObject(collectionValues);
        await _distributedCache.SetStringAsync(_key, newValue, options);
    }

    public async Task RemoveKey(string key)
    {
        var _key = key.ToLower();
        await _distributedCache.RemoveAsync(_key);
    }

    public List<string> GetKeys(string filter)
    {
        List<string> response = new();
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConfigurations.ConnectionString);
        var ep = redis.GetEndPoints();
        var server = redis.GetServer(endpoint: ep[0]);

        foreach (var key in server.Keys(database:0, pattern: $"{filter}*", pageSize: 1000))
        {
            response.Add(key.ToString());
        }

        return response;
    }
}
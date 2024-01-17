using Microsoft.Extensions.Caching.Distributed;

namespace TesteKonsi.Application.Contracts.Repository.Cache;

public interface ICacheRepository
{
    Task<T> GetValue<T>(string key);
    Task<IEnumerable<T>> GetCollection<T>(string collectionKey);
    Task SetValue<T>(string key, T value, DistributedCacheEntryOptions options = null);
    Task SetCollection<T>(string collectionKey, IEnumerable<T> collectionValues, DistributedCacheEntryOptions options = null);
    Task RemoveKey(string key);
    List<string> GetKeys(string filter);
}
using Microsoft.Extensions.Caching.Memory;
using TrackHub.Scraper.Models;

namespace TrackHub.Scraper.Cache;

internal class InMemoryCache : IScraperCache
{
    private const int TTL = 5000;

    private readonly IMemoryCache _memoryCache;
    
    public InMemoryCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Add(CacheItem item)
    {
        if (!item.Values.Any())
            return;

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMilliseconds(TTL)
        };

        string composedKey = GetComposedKey(item.Key);
        if (!_memoryCache.TryGetValue(composedKey, out _))
        {
            _memoryCache.Set<string[]>(composedKey, item.Values);
        }        
    }

    public string[]? Get(CacheKey key)
    {
        if (_memoryCache.TryGetValue(GetComposedKey(key), out string[]? cachedData))
        {
            return cachedData!;
        }

        return null;
    }

    private string GetComposedKey(CacheKey key)
    {
        return $"{key.SetIdentifier}:{key.Pattern}";
    }
}

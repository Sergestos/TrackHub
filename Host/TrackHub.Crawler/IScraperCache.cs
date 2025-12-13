using TrackHub.Service.Scraper.Models;

namespace TrackHub.Service.Scraper;

internal interface IScraperCache
{
    string[]? Get(CacheKey key);

    void Add(CacheItem item);

    void Remove(CacheKey key);
}

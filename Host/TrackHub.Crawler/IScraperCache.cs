using TrackHub.Seacher.Models;

namespace TrackHub.Scraper;

internal interface IScraperCache
{
    string[]? Get(CacheKey key);

    void Add(CacheItem item);
}

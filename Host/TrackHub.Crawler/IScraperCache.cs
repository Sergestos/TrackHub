using TrackHub.Service.Scrapper.Models;

namespace TrackHub.Service.Scrapper;

internal interface IScraperCache
{
    string[]? Get(CacheKey key);

    void Add(CacheItem item);
}

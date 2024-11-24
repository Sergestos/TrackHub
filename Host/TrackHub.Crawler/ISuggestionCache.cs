using TrackHub.Seacher.Models;

namespace TrackHub.Searcher;

internal interface ISuggestionCache
{
    string[]? Get(CacheKey key);

    void Add(CacheItem item);
}

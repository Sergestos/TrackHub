using Microsoft.Extensions.DependencyInjection;
using TrackHub.Seacher.Cache;
using TrackHub.Searcher.Searchers.Authors;
using TrackHub.Searcher.Searchers.Song;

namespace TrackHub.Searcher;

public static class ServiceCollectionExtensions
{
    public static void AddCrawlerServices(this IServiceCollection services)
    {
        services.AddScoped<ISearcherFacade, SearcherFacade>();
        services.AddScoped<IAuthorSearcher, AuthorSearcher>();
        services.AddScoped<ISongSearcher, SongSearcher>();
        services.AddSingleton<ISuggestionCache, InMemoryCache>();

        services.AddMemoryCache();
    }
}
 
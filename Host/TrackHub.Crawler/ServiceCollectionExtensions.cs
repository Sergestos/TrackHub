using Microsoft.Extensions.DependencyInjection;
using TrackHub.Searcher.Searchers.Authors;
using TrackHub.Searcher.Searchers.Song;

namespace TrackHub.Searcher;

public static class ServiceCollectionExtensions
{
    public static void AddCrawlerServices(this IServiceCollection services)
    {
        services.AddScoped<ISearcherFacade, SearcherFacade>();
        services.AddTransient<IAuthorSearcher, AuthorSearcher>();
        services.AddTransient<ISongSearcher, SongSearcher>();
    }
}
 
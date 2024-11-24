using Microsoft.Extensions.DependencyInjection;
using TrackHub.Seacher.Cache;
using TrackHub.Scraper.Searchers.Authors;
using TrackHub.Scraper.Searchers.Song;

namespace TrackHub.Scraper;

public static class ServiceCollectionExtensions
{
    public static void AddScraperServices(this IServiceCollection services)
    {
        services.AddScoped<IScraperFacade, ScraperFacade>();
        services.AddScoped<IAuthorSearcher, AuthorSearcher>();
        services.AddScoped<ISongSearcher, SongSearcher>();
        services.AddSingleton<IScraperCache, InMemoryCache>();

        services.AddMemoryCache();
    }
}
 
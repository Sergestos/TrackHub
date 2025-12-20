using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Scraper.Cache;
using TrackHub.Service.Scraper.Searchers.Authors;
using TrackHub.Service.Scraper.Searchers.Song;

namespace TrackHub.Service.Scraper;

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
 
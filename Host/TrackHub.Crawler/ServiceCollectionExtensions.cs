using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Scrapper.Cache;
using TrackHub.Service.Scrapper.Searchers.Authors;
using TrackHub.Service.Scrapper.Searchers.Song;

namespace TrackHub.Service.Scrapper;

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
 
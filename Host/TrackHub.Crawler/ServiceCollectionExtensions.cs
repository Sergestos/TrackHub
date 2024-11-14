using Microsoft.Extensions.DependencyInjection;
using TrackHub.Crawler.Searchers.Authors;
using TrackHub.Crawler.Searchers.Song;

namespace TrackHub.Crawler;

public static class ServiceCollectionExtensions
{
    public static void AddCrawlerServices(this IServiceCollection services)
    {
        services.AddScoped<ICrawlerFacade, CrawlerFacade>();
        services.AddTransient<IAuthorCrawler, AuthorCrawler>();
        services.AddTransient<ISongCrawler, SongCrawler>();
    }
}
 
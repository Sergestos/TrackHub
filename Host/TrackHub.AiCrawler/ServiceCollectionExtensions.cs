using Microsoft.Extensions.DependencyInjection;
using TrackHub.AiCrawler.OpenAI;

namespace TrackHub.AiCrawler;

public static class ServiceCollectionExtensions
{
    public static void AddAiCrawlerServices(this IServiceCollection services)
    {
        services.AddScoped<IAiMusicCrawler, OpenAIMusicCrawler>();        
    }
}

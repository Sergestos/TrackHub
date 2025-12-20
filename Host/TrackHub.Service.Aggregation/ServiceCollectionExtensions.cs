using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Aggregation.Services;

namespace TrackHub.Service.Aggregation;

public static class ServiceCollectionExtensions
{
    public static void AddAggregationServices(this IServiceCollection services)
    {
        services.AddScoped<IAggregationService, AggregationFunctionClient>();
        services.AddHttpClient<AggregationFunctionClient>(client =>
        {
            client.BaseAddress = new Uri("https://<your-function-app>.azurewebsites.net/");
        });
    }
}

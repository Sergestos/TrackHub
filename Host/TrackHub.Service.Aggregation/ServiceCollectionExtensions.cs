using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Aggregation.Services;
using TrackHub.Service.Aggregation.Transport;

namespace TrackHub.Service.Aggregation;

public static class ServiceCollectionExtensions
{
    public static void AddAggregationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IAggregationReadService, AggregationReadService>();
        services.AddScoped<IAggregationService, AggregationService>();
        services.AddHttpClient<IAggregationRequestService, AggregationFunctionClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("AzureFunction:Url").Value!);
            client.DefaultRequestHeaders.Add("x-functions-key", configuration.GetSection("AzureFunction:Key").Value!);
        });
    }
}

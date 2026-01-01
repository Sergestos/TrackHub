using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Aggregation.Services;
using TrackHub.Service.Infrastructure;
using TrackHub.Service.Services.AggregationServices;
using TrackHub.Service.Services.ExerciseServices;
using TrackHub.Service.Services.PreviewServices;
using TrackHub.Service.Services.UserServices;

namespace TrackHub.Service;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAutoMapper(typeof(ServiceMapper));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IExerciseService, ExerciseService>();
        services.AddScoped<IExerciseSearchService, ExerciseSearchService>();
        services.AddScoped<IPreviewService, PreviewService>();
        services.AddScoped<IAggregationReadService, AggregationReadService>();
        services.AddHttpClient<IAggregationService, AggregationFunctionClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("AzureFunction:Url").Value!);
            client.DefaultRequestHeaders.Add("x-functions-key", configuration.GetSection("AzureFunction:Key").Value!);
        });
    }
}

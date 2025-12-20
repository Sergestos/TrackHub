using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Aggregation.Services;
using TrackHub.Service.Infrastructure;
using TrackHub.Service.Services.ExerciseServices;
using TrackHub.Service.Services.PreviewServices;
using TrackHub.Service.Services.UserServices;

namespace TrackHub.Service;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceMapper));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IExerciseService, ExerciseService>();
        services.AddScoped<IExerciseSearchService, ExerciseSearchService>();
        services.AddScoped<IPreviewService, PreviewService>();
        services.AddScoped<IAggregationService, AggregationFunctionClient>();
        services.AddHttpClient<AggregationFunctionClient>(client =>
        {
            client.BaseAddress = new Uri("https://<your-function-app>.azurewebsites.net/");
        });
    }
}

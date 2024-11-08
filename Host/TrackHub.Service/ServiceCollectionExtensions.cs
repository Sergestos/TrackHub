using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Infrastructure;
using TrackHub.Service.Services.ExerciseServices;
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
    }
}

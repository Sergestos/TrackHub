using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.ExerciseServices;
using TrackHub.Service.UserServices;

namespace TrackHub.Service;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IExerciseService, ExerciseService>();
    }
}

using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.ExerciseServices;
using TrackHub.Service.Infrastructure;
using TrackHub.Service.UserServices;

namespace TrackHub.Service;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceMapper));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IExerciseService, ExerciseService>();
    }
}

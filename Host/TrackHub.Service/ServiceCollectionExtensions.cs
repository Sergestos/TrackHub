using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Exercises.Infrastructure;

namespace TrackHub.Service.Exercises;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cgf => { }, typeof(ServiceMapper));

        services.AddScoped<IExerciseService, ExerciseService>();
        services.AddScoped<IExerciseSearchService, ExerciseSearchService>();
    }
}

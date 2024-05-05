using TrackHub.Domain;
using TrackHub.Service.Exercises.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace TrackHub.Service.Exercises;

public static class ServiceCollectionExtensions
{
    public static void AddExerciseServices(this IServiceCollection services)
    {
        services.AddScoped<ISearchable<Song>, SongSearchService>();        
        services.AddScoped<ISearchable<Author>, AuthorSearchService>();
    }
}

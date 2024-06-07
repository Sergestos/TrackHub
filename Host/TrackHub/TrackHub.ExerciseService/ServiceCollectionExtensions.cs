using TrackHub.CosmosDb;
using TrackHub.Domain.Data;
using TrackHub.ExerciseService.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace TrackHub.ExerciseService;

public static class ServiceCollectionExtensions
{
    public static void AddExerciseServices(this IServiceCollection services, IConfiguration configuration)
    {
     //   services.Configure<CosmosClientOptions>(configuration.GetSection("CosmosDb"));

        services.AddSingleton<ICosmosDbContext, CosmosDbClient>();
       // services.AddScoped<IExerciseRepository, ExerciseRepository>();

        services.AddAutoMapper(typeof(ExerciseMappingProfile));
    }
}

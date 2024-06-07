using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackHub.CosmosDb;
using TrackHub.Domain.Data;
using TrackHub.Domain.Data.Repositories;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domaim.Data;

public static class ServiceCollectionExtensions
{
    public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<CosmosClientOptions>(configuration.GetSection("CosmosDb"));

        services.AddSingleton<ICosmosDbContext, CosmosDbClient>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();       
    }
}

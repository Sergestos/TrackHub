using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackHub.CosmosDb;
using TrackHub.Domain.Data.Repositories;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data;

public static class ServiceCollectionExtensions
{
    public static void AddDataServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        //services.AddOptions<CosmosClientOptions>("CosmosDb");        
        //services.Configure<CosmosClientOptions>(configuration.GetSection("CosmosDb"));

        services.AddSingleton<ICosmosDbContext, CosmosDbClient>();

        services.AddTransient<IExerciseRepository, ExerciseRepository>();       
        services.AddTransient<IRecordRepository, RecordRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}

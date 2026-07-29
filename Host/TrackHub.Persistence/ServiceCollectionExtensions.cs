using Microsoft.Extensions.DependencyInjection;
using TrackHub.CosmosDb;
using TrackHub.Domain.Repositories;
using TrackHub.Persistence.CosmosDb.Repositories;
using TrackHub.Persistence.Repositories;

namespace TrackHub.Persistence.CosmosDb;

public static class ServiceCollectionExtensions
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddSingleton<ICosmosDbContext, CosmosDbClient>();

        services.AddTransient<IExerciseRepository, ExerciseRepository>();       
        services.AddTransient<IRecordRepository, RecordRepository>();
        services.AddTransient<IAggregationRepository, AggregationRepository>();
    }
}

using Microsoft.Extensions.DependencyInjection;
using TrackHub.Domain.Repositories;
using TrackHub.Infrastructure.Sql.Context;
using TrackHub.Persistence.Sql.Repositories;

namespace TrackHub.Persistence.CosmosDb;

public static class ServiceCollectionExtensions
{
    public static void AddSqlDataServices(this IServiceCollection services, string connectionString)
    {
        services.AddSqlServer<TrackHubDbContext>(connectionString);

        services.AddTransient<IUserRepository, UserRepository>();
    }
}

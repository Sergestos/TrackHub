using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackHub.Domain.Repositories;
using TrackHub.Persistence.Sql.Context;
using TrackHub.Persistence.Sql.Repositories;

namespace TrackHub.Persistence.Sql;

public static class ServiceCollectionExtensions
{
    public static void AddSqlDataServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TrackHubDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sql => sql.MigrationsAssembly("TrackHub.Persistence.Sql.Migration")));

        services.AddScoped<ITrackHubDbContext>(provider =>
            provider.GetRequiredService<TrackHubDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
    }
}

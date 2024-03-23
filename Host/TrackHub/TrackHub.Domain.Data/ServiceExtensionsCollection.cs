using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackHub.Data.Cassandra;

namespace TrackHub.Domain.Data;

public static class ServiceExtensionsCollection
{
    public static void AddCassandraDb(this IServiceCollection services, IConfiguration configuration) 
    {
        var task = CassandraBuilder.InitializeCassandraSession(configuration);
        task.Wait();
    }
}

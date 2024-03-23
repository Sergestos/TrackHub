using Cassandra;
using Microsoft.Extensions.Configuration;
using System.Net.Security;
using System.Security.Authentication;

namespace TrackHub.Data.Cassandra;

public static class CassandraBuilder
{
    public static ISession? Session { get; private set; }

    public static async Task InitializeCassandraSession(IConfiguration configuration)
    {
        var cassandraConfiguration = configuration.GetSection("Cassandra");

        var options = new SSLOptions(
            SslProtocols.Tls12,
            true,
            (_, _, _, sslPolicyError) => sslPolicyError == SslPolicyErrors.None);
        options.SetHostNameResolver((ipAddress) => cassandraConfiguration["ContactPoint"]);

        Cluster cluster = Cluster
            .Builder()
            .WithCredentials(cassandraConfiguration["DbUserName"], cassandraConfiguration["DbPassword"])
            .WithPort(int.Parse(cassandraConfiguration["DbPort"]!))
            .AddContactPoint(cassandraConfiguration["ContactPoint"])
            .WithSSL(options)
            .Build();

        Session = await cluster.ConnectAsync();        
    }
}

using Cassandra;
using System.Net.Security;
using System.Security.Authentication;

namespace TrackHub.Domain.Data;

internal static class CassandraBuilder
{
    private const string UserName = "trackhub-db";
    private const string CassandraContactPoint = "trackhub-db.cassandra.cosmos.azure.com";
    private const string Password = "7HD1rPwl6INiAlJ5KuymnjxC09HCOGPXFzmh09b4oisKQ7VL5GJFssCH22PM0uSTYR8IprYlMt7AACDbbp2aLA==";
    private const int Port = 10350;

    public static ISession? Session;

    public static async Task InitializeCassandraSession()
    {
        var options = new SSLOptions(
            SslProtocols.Tls12, 
            true,
            (_, _, _, sslPolicyError) => sslPolicyError == SslPolicyErrors.None);
        options.SetHostNameResolver((ipAddress) => CassandraContactPoint);

        Cluster cluster = Cluster
            .Builder()
            .WithCredentials(UserName, Password)
            .WithPort(Port)
            .AddContactPoint(CassandraContactPoint)
            .WithSSL(options)
        .Build();

        Session = await cluster.ConnectAsync();
    }
}

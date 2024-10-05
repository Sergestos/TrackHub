using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Options;
using TrackHub.CosmosDb;

namespace TrackHub.Domain.Data;

public class CosmosDbClient : ICosmosDbContext
{
    private CosmosClientOptions _options;

    private CosmosClient? _client;
    private Container? _container;    

    private CosmosClient Client => _client ??= new CosmosClientBuilder(_options.AccountEndpoint).Build();

    public CosmosDbClient(IOptionsMonitor<CosmosClientOptions> options)
    {
        _options = options.CurrentValue;     
    }    

    public Container GetContainer(string containerName)
    {
        var dateBase = Client.GetDatabase(_options.DateBaseName);
        return dateBase.GetContainer(containerName);
    }
}

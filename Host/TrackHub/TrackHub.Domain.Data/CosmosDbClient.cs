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

    private CosmosClient Client => _client ??= new CosmosClientBuilder(_options.AccoutEndpoint).Build();

    public Container Container => _container ??= GetContainerFromOptions();

    public CosmosDbClient(IOptionsMonitor<CosmosClientOptions> options)
    {
        _options = options.CurrentValue;     
    }    

    private Container GetContainerFromOptions()
    {
        var dateBase = Client.GetDatabase(_options.DateBaseName);
        return dateBase.GetContainer(_options.ExerciseContainerName);
    }
}

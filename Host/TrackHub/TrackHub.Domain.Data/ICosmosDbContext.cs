using Microsoft.Azure.Cosmos;

namespace TrackHub.CosmosDb;

public interface ICosmosDbContext
{    
    Container GetContainer(string containerName);
}

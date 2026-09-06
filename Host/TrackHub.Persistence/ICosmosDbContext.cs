using Microsoft.Azure.Cosmos;

namespace TrackHub.Persistence.CosmosDb;

public interface ICosmosDbContext
{    
    Container GetContainer(string containerName);
}

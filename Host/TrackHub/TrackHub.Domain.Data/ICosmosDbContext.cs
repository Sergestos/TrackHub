using Microsoft.Azure.Cosmos;

namespace TrackHub.CosmosDb;

public interface ICosmosDbContext
{
    Container Container { get; }
}

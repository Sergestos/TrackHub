using Azure.Identity;
using Microsoft.Azure.Cosmos;

namespace TrackHub.Data.CosmosDb;

public class TrackHubDbContextBuilder
{
    public static void Build()
    {
        CosmosClient client = new(
            accountEndpoint: "https://trackhub-db.documents.azure.com:443/",
            tokenCredential: new DefaultAzureCredential());
    }
}

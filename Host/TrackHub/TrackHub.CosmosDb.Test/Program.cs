using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;
using TrackHub.CosmosDb.Test;

//const string accountEndpoint = "https://trackhub-db.documents.azure.com:443/;AccountKey=qxm1o8lxLxAY1quu57PWpqQTfaVEOUDAmt88ApVry8f0fGsMN4GixrzLNdC5RI474mqor57GK0GuACDbfTMiWQ==";
const string accountEndpoint = "AccountEndpoint=https://trackhub-db.documents.azure.com:443/;AccountKey=qxm1o8lxLxAY1quu57PWpqQTfaVEOUDAmt88ApVry8f0fGsMN4GixrzLNdC5RI474mqor57GK0GuACDbfTMiWQ==;";
CosmosSerializationOptions serializerOptions = new()
{
    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
};

using CosmosClient client = new CosmosClientBuilder(accountEndpoint)
    .WithSerializerOptions(serializerOptions)
    .Build();

Console.WriteLine($"[CosmosClient created]:\t{client.Endpoint}, {client.ClientOptions.ApplicationName}");

Database dataBase = client.CreateDatabaseIfNotExistsAsync(
    id: "TrackHubDb").Result;

ContainerProperties properties = new(
    id: "exercise",
    partitionKeyPath: "/user_id");

var throughput = ThroughputProperties.CreateAutoscaleThroughput(
    autoscaleMaxThroughput: 1000
);

Container container = dataBase.CreateContainerIfNotExistsAsync(
    containerProperties: properties,
    throughputProperties: throughput
).Result;

Console.WriteLine($"[Container obtained]:\t{container.Id}");

var exerciseModel = new Exercise()
{
    Id = "102",
    Songs = [
        new Song()
        {
            SongName = "heavy"
        },
        new Song()
        {
            SongName = "light"
        }
    ],
    UserId = "exodus@gmail.com"
};

var result = await container.CreateItemAsync(exerciseModel, new PartitionKey("exodus@gmail.com"), null, CancellationToken.None);
Console.WriteLine(result.StatusCode);

Console.ReadKey();

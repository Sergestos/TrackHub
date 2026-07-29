namespace TrackHub.Persistence.CosmosDb;

public record CosmosClientOptions
{
    public required string AccountEndpoint { get; set; }

    public required string DateBaseName { get; set; }

    public required string UserContainerName { get; set; }

    public required int AutoscaleMaxThroughput { get; set; }
}


namespace TrackHub.Domain.Data;

public record CosmosClientOptions
{
    public required string AccoutEndpoint { get; set; }

    public required string DateBaseName { get; set; }

    public required string ExerciseContainerName { get; set; }

    public required string UserContainerName { get; set; }

    public required int AutoscaleMaxThroughput { get; set; }
}

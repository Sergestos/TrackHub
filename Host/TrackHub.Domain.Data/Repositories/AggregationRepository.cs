using System.Net;
using Microsoft.Azure.Cosmos;
using TrackHub.CosmosDb;
using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal class AggregationRepository : IAggregationRepository
{
    private const string AggregationContainerType = "aggregation";

    private readonly Container _container;

    public AggregationRepository(ICosmosDbContext context)
    {
        _container = context.GetContainer(AggregationContainerType);
    }

    public async Task<ExerciseAggregation?> GetExerciseAggregationById(string aggregationId, string userId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _container.ReadItemAsync<ExerciseAggregation>(
                aggregationId,
                new PartitionKey(userId),
                cancellationToken: cancellationToken);

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<SongAggregation?> GetSongAggregationById(string aggregationId, string userId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _container.ReadItemAsync<SongAggregation>(
                aggregationId,
                new PartitionKey(userId),
                cancellationToken: cancellationToken);

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<ExerciseAggregation> UpsertExerciseAggregation(string userId, ExerciseAggregation aggregation, CancellationToken cancellationToken)
    {
        var response = await _container.UpsertItemAsync(
            aggregation,
            new PartitionKey(userId),
            cancellationToken: cancellationToken);

        return response.Resource;
    }

    public async Task<SongAggregation> UpsertSongAggregation(string userId, SongAggregation aggregation, CancellationToken cancellationToken)
    {
        var response = await _container.UpsertItemAsync(
            aggregation,
            new PartitionKey(userId),
            cancellationToken: cancellationToken);

        return response.Resource;
    }
}
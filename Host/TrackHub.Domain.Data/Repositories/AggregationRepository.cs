using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Net;
using TrackHub.CosmosDb;
using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Consistency;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal class AggregationRepository : IAggregationRepository
{
    private const string AggregationContainerType = "aggregation";
    private const string ExerciseType = "exercise_aggregation";
    private const string SongType = "song_aggregation";

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

    public async Task<IEnumerable<ExerciseAggregation>?> GetExerciseAggregationListByIds(string[] aggregationIds, string userId, CancellationToken cancellationToken)
    {
        if (aggregationIds is null) throw new ArgumentNullException(nameof(aggregationIds));

        var ids = aggregationIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct(StringComparer.Ordinal)
            .ToList();

        if (ids.Count == 0)
            return Enumerable.Empty<ExerciseAggregation>();

        IReadOnlyList<(string id, PartitionKey partitionKey)> items =
            ids.Select(id => (id, new PartitionKey(userId)))
               .ToList();

        try
        {
            var response = await _container.ReadManyItemsAsync<ExerciseAggregation>(
                items,
                cancellationToken: cancellationToken);

            return response.Resource.ToList();
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return Enumerable.Empty<ExerciseAggregation>();
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

    public async Task<IEnumerable<SongAggregation>> GetSongAggregationListByIds(string userId, string[] songAggregationIds, CancellationToken cancellationToken)
    {
        if (songAggregationIds is null) throw new ArgumentNullException(nameof(songAggregationIds));

        var ids = songAggregationIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct(StringComparer.Ordinal)       
            .Select(x => AggregationIds.Song(userId, x))
            .ToList();

        if (ids.Count == 0)
            return Enumerable.Empty<SongAggregation>();

        IReadOnlyList<(string id, PartitionKey partitionKey)> items =
            ids.Select(id => (id, new PartitionKey(userId)))
               .ToList();

        try
        {
            var response = await _container.ReadManyItemsAsync<SongAggregation>(
                items,
                cancellationToken: cancellationToken);

            return response.Resource.ToList();
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return Enumerable.Empty<SongAggregation>();
        }
    }

    public async Task<IEnumerable<SongAggregation>> GetSongAggregationsByUserId(string userId, CancellationToken cancellationToken)
    {
        var matches = _container.GetItemLinqQueryable<SongAggregation>()
            .Where(x => x.UserId == userId && x.Type == SongType);

        using (FeedIterator<SongAggregation> linqFeed = matches.ToFeedIterator())
        {
            var result = new List<SongAggregation>();
            while (linqFeed.HasMoreResults)
            {
                FeedResponse<SongAggregation> iterationResponse = await linqFeed.ReadNextAsync();
                foreach (SongAggregation item in iterationResponse)
                {
                    result.Add(item);
                }
            }

            return result;
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

    public async Task<IEnumerable<SongAggregation>> UpsertSongAggregations(string userId, SongAggregation[] aggregations, CancellationToken cancellationToken)
    {
        var upsertedAggregations = new List<SongAggregation>();

        var tasks = aggregations.Select(async doc =>
       {
           var response = await _container.UpsertItemAsync(
               doc,
               new PartitionKey(userId),
               cancellationToken: cancellationToken);

           upsertedAggregations.Add(response.Resource);
       });

        await Task.WhenAll(tasks);

        return upsertedAggregations;
    }
}
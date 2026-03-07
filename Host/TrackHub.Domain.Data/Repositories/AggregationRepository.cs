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
    private const string SongType = "song_aggregation";

    private readonly Container _container;

    public AggregationRepository(ICosmosDbContext context)
    {
        _container = context.GetContainer(AggregationContainerType);
    }

    public async Task<ExerciseAggregation?> GetExerciseAggregationByIdAsync(string aggregationId, string userId, CancellationToken cancellationToken)
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

    public async Task<IEnumerable<ExerciseAggregation>?> GetExerciseAggregationListByIdsAsync(string[] aggregationIds, string userId, CancellationToken cancellationToken)
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

    public async Task<SongAggregation?> GetSongAggregationByIdAsync(string aggregationId, string userId, CancellationToken cancellationToken)
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

    public async Task<IEnumerable<SongAggregation>> GetSongAggregationListByDateAsync(string userId, DateOnly date, CancellationToken cancellationToken)
    {
        var query = new QueryDefinition(@"
            SELECT DISTINCT
                VALUE c
            FROM c
            JOIN d 
                IN c.date_aggregation
            WHERE 
                c.user_id = @userId
                AND d.Year = @year
                AND d.Month = @month")
            .WithParameter("@userId", userId)
            .WithParameter("@year", date.Year)
            .WithParameter("@month", date.Month);

            var results = new List<SongAggregation>();

            using FeedIterator<SongAggregation> iterator =
                _container.GetItemQueryIterator<SongAggregation>(query);

            while (iterator.HasMoreResults)
            {
                FeedResponse<SongAggregation> response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

        return results;
    }

    public async Task<IEnumerable<SongAggregation>> GetSongAggregationListByIdsAsync(string userId, string[] songAggregationIds, CancellationToken cancellationToken)
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

    public async Task<IEnumerable<SongAggregation>> GetSongAggregationsByUserIdAsync(string userId, CancellationToken cancellationToken)
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

    public async Task<ExerciseAggregation> UpsertExerciseAggregationAsync(string userId, ExerciseAggregation aggregation, CancellationToken cancellationToken)
    {
        var response = await _container.UpsertItemAsync(
            aggregation,
            new PartitionKey(userId),
            cancellationToken: cancellationToken);

        return response.Resource;
    }

    public async Task<SongAggregation> UpsertSongAggregationAsync(string userId, SongAggregation aggregation, CancellationToken cancellationToken)
    {
        var response = await _container.UpsertItemAsync(
            aggregation,
            new PartitionKey(userId),
            cancellationToken: cancellationToken);

        return response.Resource;
    }

    public async Task<IEnumerable<SongAggregation>> UpsertSongAggregationsAsync(string userId, SongAggregation[] aggregations, CancellationToken cancellationToken)
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
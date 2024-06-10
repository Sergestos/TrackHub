using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using TrackHub.CosmosDb;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal class ExerciseRepository : IExerciseRepository
{
    private const string ExerciseContainerType = "exercise";

    private readonly ICosmosDbContext _context;

    public ExerciseRepository(ICosmosDbContext context)
    {
        _context = context;
    }

    public async Task UpsertExerciseAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        await _context.Container.UpsertItemAsync(exercise, new PartitionKey(exercise.UserId), null, cancellationToken);
    }

    public async Task DeleteExerciseAsync(string exerciseId, string userId, CancellationToken cancellationToken)
    {
        await _context.Container.DeleteItemAsync<Exercise>(exerciseId, new PartitionKey(userId), new ItemRequestOptions(), cancellationToken);
    }

    public async Task<Exercise> GetExerciseByIdAsync(string exerciseId, string userId, CancellationToken cancellationToken)
    {
        var response = await _context.Container.ReadItemAsync<Exercise>(exerciseId, new PartitionKey(userId), null, cancellationToken);

        return response.Resource;
    }

    public async Task<IEnumerable<Exercise>> GetExerciseListByUserAsync(string userId, CancellationToken cancellationToken)
    {                
        var matches = _context.Container.GetItemLinqQueryable<Exercise>()
            .Where(x => x.UserId == userId && x.EntityType == ExerciseContainerType);

        using (FeedIterator<Exercise> linqFeed = matches.ToFeedIterator())
        {
            var result = new List<Exercise>();
            while (linqFeed.HasMoreResults)
            {
                FeedResponse<Exercise> iterationResponse = await linqFeed.ReadNextAsync();
                foreach (Exercise item in iterationResponse)
                {
                    result.Add(item);
                }
            }

            return result;
        }            
    }
}

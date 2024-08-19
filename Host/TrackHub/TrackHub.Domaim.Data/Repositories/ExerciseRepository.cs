using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using TrackHub.CosmosDb;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal class ExerciseRepository : IExerciseRepository
{
    private const string ExerciseContainerType = "exercise";

    private readonly Container _container;

    public ExerciseRepository(ICosmosDbContext context)
    {
        _container = context.GetContainer(ExerciseContainerType);
    }

    public async Task<Exercise> UpsertExerciseAsync(Exercise exercise, CancellationToken cancellationToken) 
    {
        return await _container.UpsertItemAsync(exercise, new PartitionKey(exercise.UserId), null, cancellationToken);
    }

    public async Task<Exercise?> GetExerciseByIdAsync(string exerciseId, string userId, CancellationToken cancellationToken)
    {
        ItemResponse<Exercise>? response = null;

        try
        {
            response = await _container.ReadItemAsync<Exercise>(exerciseId, new PartitionKey(userId), null, cancellationToken);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) { }

        return response?.Resource;
    }

    public async Task<IEnumerable<Exercise>> GetExercisesByDateAsync(int year, int month, string userId, CancellationToken cancellationToken)
    {
        var matches = _container.GetItemLinqQueryable<Exercise>()
            .Where(x => x.UserId == userId && x.PlayDate.Year == year && x.PlayDate.Month == month);

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

    public async Task<IEnumerable<Exercise>> GetExerciseListByUserAsync(string userId, CancellationToken cancellationToken)
    {                
        var matches = _container.GetItemLinqQueryable<Exercise>()
            .Where(x => x.UserId == userId);

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

    public Exercise? GetExerciseByDate(DateOnly date, string userId, CancellationToken cancellationToken)
    {
        var result = _container.GetItemLinqQueryable<Exercise>()
            .Where(x => x.UserId == userId && 
                   x.PlayDate.Year == date.Year && x.PlayDate.Month == date.Month && x.PlayDate.Day == date.Day)
            .FirstOrDefault();

        return result;
    }

    public async Task DeleteExerciseAsync(string exerciseId, string userId, CancellationToken cancellationToken)
    {
        await _container.DeleteItemAsync<Exercise>(exerciseId, new PartitionKey(userId), new ItemRequestOptions(), cancellationToken);
    }
}

using Microsoft.Azure.Cosmos;
using TrackHub.CosmosDb;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal class ExerciseRepository : IExerciseRepository
{
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

    public Task<Exercise> GetExerciseByIdAsync(string exerciseId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Exercise>> GetExerciseListByUserAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }    
}

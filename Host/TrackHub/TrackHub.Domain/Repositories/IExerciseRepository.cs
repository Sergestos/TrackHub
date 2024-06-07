using TrackHub.Domain.Entities;

namespace TrackHub.Domain.Repositories;

public interface IExerciseRepository
{
    Task<Exercise> GetExerciseByIdAsync(string exerciseId, CancellationToken cancellationToken);

    Task<IEnumerable<Exercise>> GetExerciseListByUserAsync(string userId, CancellationToken cancellationToken);

    Task UpsertExerciseAsync(Exercise exercise, CancellationToken cancellationToken);

    Task DeleteExerciseAsync(string exerciseId, string userId, CancellationToken cancellationToken);
}

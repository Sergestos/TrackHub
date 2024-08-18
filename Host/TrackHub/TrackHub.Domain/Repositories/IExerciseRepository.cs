using TrackHub.Domain.Entities;

namespace TrackHub.Domain.Repositories;

public interface IExerciseRepository
{
    Exercise? GetExerciseByDate(DateOnly date, string userId, CancellationToken cancellationToken);

    Task<Exercise?> GetExerciseByIdAsync(string exerciseId, string userId, CancellationToken cancellationToken);

    Task<IEnumerable<Exercise>> GetExerciseListByUserAsync(string userId, CancellationToken cancellationToken);

    Task<Exercise> UpsertExerciseAsync(Exercise exercise, CancellationToken cancellationToken);

    Task DeleteExerciseAsync(string exerciseId, string userId, CancellationToken cancellationToken);
}

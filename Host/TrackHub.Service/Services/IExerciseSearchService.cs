using TrackHub.Service.Exercises.Models;

namespace TrackHub.Service.Exercises;

public interface IExerciseSearchService
{
    Task<IEnumerable<ExerciseListItem>> GetExercisesByDateAsync(int? year, int? month, string userId, CancellationToken cancellationToken);
}

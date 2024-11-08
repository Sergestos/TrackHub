using TrackHub.Service.Services.ExerciseServices.Models;

namespace TrackHub.Service.Services.ExerciseServices;

public interface IExerciseSearchService
{
    Task<IEnumerable<ExerciseListItem>> GetExercisesByDateAsync(int? year, int? month, string userId, CancellationToken cancellationToken);
}

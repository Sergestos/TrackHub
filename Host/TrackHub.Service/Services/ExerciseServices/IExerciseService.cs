using TrackHub.Domain.Entities;
using TrackHub.Service.Services.ExerciseServices.Models;

namespace TrackHub.Service.Services.ExerciseServices;

public interface IExerciseService
{
    Task<Exercise> CreateExerciseAsync(CreateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken);

    Task<Exercise> UpdateExerciseAsync(UpdateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken);

    Task<Exercise> DeleteExerciseAsync(string exerciseId, string[] recordIds, string userId, CancellationToken cancellationToken);
}

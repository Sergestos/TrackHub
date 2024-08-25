using TrackHub.Domain.Entities;
using TrackHub.Service.ExerciseServices.Models;

namespace TrackHub.Service.ExerciseServices;

public interface IExerciseService
{
    Task<Exercise> CreateExerciseAsync(CreateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken);

    Task<Exercise> UpdateExerciseAsync(UpdateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken);
}

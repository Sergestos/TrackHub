using TrackHub.Domain.Entities;
using TrackHub.Service.ExerciseServices.Models;

namespace TrackHub.Service.ExerciseServices;

public interface IExerciseService
{
    Task<Exercise> CreateExercise(CreateExerciseModel exerciseModel, string userEmail, CancellationToken cancellationToken);
}

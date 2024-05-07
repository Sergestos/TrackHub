using TrackHub.Contract.Inputs;
using TrackHub.Contract.ViewModels;

namespace TrackHub.Contract;

public interface IExerciseRepository
{
    Task CreateAsync(ExerciseCreateModel model, CancellationToken cancellationToken);

    Task UpdateAsync(ExerciseUpdateModel model, CancellationToken cancellationToken);

    Task<ExerciseDetailsViewModel> GetByIdAsync(string exerciseId, CancellationToken cancellationToken);

    Task<IEnumerable<ExericeViewModel>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken);
}

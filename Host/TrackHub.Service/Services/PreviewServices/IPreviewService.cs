
using TrackHub.Service.Services.PreviewServices.Models;

namespace TrackHub.Service.Services.PreviewServices;

public interface IPreviewService
{
    Task<PreviewValidationModel> PreviewExerciseAsync(string previewText, CancellationToken cancellationToken);
}
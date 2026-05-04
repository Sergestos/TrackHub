
using TrackHub.Application.Preview.Models;

namespace TrackHub.Application.Preview;

public interface IPreviewService
{
    Task<PreviewStateModel> PreviewExerciseAsync(string previewText, CancellationToken cancellationToken);
}
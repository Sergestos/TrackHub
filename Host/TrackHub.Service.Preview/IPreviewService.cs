
using TrackHub.Application.Service.Preview.Models;

namespace TrackHub.Application.Service.Preview;

public interface IPreviewService
{
    Task<PreviewStateModel> PreviewExerciseAsync(string previewText, CancellationToken cancellationToken);
}
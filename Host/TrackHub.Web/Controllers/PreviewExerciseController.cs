using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Service.Services.PreviewServices;
using TrackHub.Service.Services.PreviewServices.Models;
using TrackHub.Web.Models;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/preview")]
public class PreviewExerciseController : TrackHubController
{
    private readonly IPreviewService _previewService;

    public PreviewExerciseController(IPreviewService previewService)
    {
        _previewService = previewService;
    }

    [HttpPost]    
    [ProducesResponseType(typeof(PreviewStateModel), 200)]
    public async Task<IActionResult> PreviewExerciseAsync([FromBody] PreviewModel model, CancellationToken cancellationToken)
    {
        var result = await _previewService.PreviewExerciseAsync(model.PreviewText, cancellationToken);

        return Ok(result);
    }
}

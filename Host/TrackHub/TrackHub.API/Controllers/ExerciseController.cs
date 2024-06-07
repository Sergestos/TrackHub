/*using Microsoft.AspNetCore.Mvc;
namespace TrackHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ExerciseController : Controller
{
    private readonly IExerciseRepository _exerciseRepository;

    public ExerciseController(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ExerciseDetailsViewModel), 200)]
    public async Task<IActionResult> GetByIdAsync([FromQuery] string exerciseId, CancellationToken cancellationToken)
    {
        var result = await _exerciseRepository.GetByIdAsync(exerciseId, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(IEnumerable<ExericeViewModel>), 200)]
    public async Task<IActionResult> GetAllAsync([FromQuery] string userId, CancellationToken cancellationToken)
    {
        var result = await _exerciseRepository.GetAllByUserIdAsync(userId, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostAsync([FromBody] ExerciseCreateModel model, CancellationToken cancellationToken)
    {
        await _exerciseRepository.CreateAsync(model, cancellationToken);

        return StatusCode(201);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateAsync([FromBody] ExerciseUpdateModel model, CancellationToken cancellationToken)
    {
        await _exerciseRepository.UpdateAsync(model, cancellationToken);

        return Ok();
    }
}
*/
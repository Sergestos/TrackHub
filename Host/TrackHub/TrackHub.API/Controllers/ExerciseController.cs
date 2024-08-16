using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
namespace TrackHub.API.Controllers;

[Authorize]
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
    [ProducesResponseType(typeof(Exercise), 200)]
    public async Task<IActionResult> GetByIdAsync([FromQuery] string exerciseId, CancellationToken cancellationToken)
    {
        var email = User.Claims.First(claim => claim.Type! == ClaimTypes.Email).Value;            
        var result = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, email, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Exercise model, CancellationToken cancellationToken)
    {     
        await _exerciseRepository.UpsertExerciseAsync(model, cancellationToken);

        return StatusCode(201);
    }
}

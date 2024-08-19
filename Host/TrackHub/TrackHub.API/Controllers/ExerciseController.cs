using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Service.ExerciseServices;
using TrackHub.Service.ExerciseServices.Models;

namespace TrackHub.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExerciseController : Controller
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService, IExerciseRepository exerciseRepository)
    {
        _exerciseService = exerciseService;
        _exerciseRepository = exerciseRepository;
    }

    /* [HttpGet]
     [ProducesResponseType(typeof(Exercise), 200)]
     public async Task<IActionResult> GetByIdAsync([FromQuery] string exerciseId, CancellationToken cancellationToken)
     {
         var email = User.Claims.First(claim => claim.Type! == ClaimTypes.Email).Value;            
         var result = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, email, cancellationToken);

         return Ok(result);
     }*/

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Exercise>), 200)]
    public async Task<IActionResult> GetExercisesByDateAsync([FromQuery] int year, [FromQuery] int month, CancellationToken cancellationToken)
    {
        var userId = User.Claims.First(claim => claim.Type! == ClaimTypes.NameIdentifier).Value;
        var result = await _exerciseRepository.GetExercisesByDateAsync(year, month, userId, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Exercise), 201)]
    public async Task<IActionResult> PostAsync([FromBody] CreateExerciseModel model, CancellationToken cancellationToken)
    {
        var userId = User.Claims.First(claim => claim.Type! == ClaimTypes.NameIdentifier).Value;
        var result = await _exerciseService.CreateExercise(model, userId, cancellationToken);

        return StatusCode(201, result);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Service.ExerciseServices;
using TrackHub.Service.ExerciseServices.Models;
using TrackHub.Web.Controllers;
using TrackHub.Web.Models;

namespace TrackHub.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExerciseController : TrackHubController
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IExerciseService _exerciseService;
    private readonly IMapper _mapper;

    public ExerciseController(IExerciseService exerciseService, IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseService = exerciseService;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

     [HttpGet]
     [ProducesResponseType(typeof(ExerciseView), 200)]
     public async Task<IActionResult> GetByIdAsync([FromQuery] string exerciseId, CancellationToken cancellationToken)
     {         
         var result = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, CurrentUserId, cancellationToken);

         return Ok(_mapper.Map<ExerciseView>( result));
     }
    
    [HttpGet]
    [Route("list")]    
    [ProducesResponseType(typeof(IEnumerable<ExerciseListItem>), 200)]
    public async Task<IActionResult> GetExercisesByDateAsync([FromQuery] int year, [FromQuery] int month, CancellationToken cancellationToken)
    {
        var result = await _exerciseRepository.GetExerciseListByDateAsync(year, month, CurrentUserId, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<ExerciseListItem>>(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Exercise), 201)]
    public async Task<IActionResult> PostAsync([FromBody] CreateExerciseModel model, CancellationToken cancellationToken)
    {
        var result = await _exerciseService.CreateExercise(model, CurrentUserId, cancellationToken);

        return StatusCode(201, result);
    }
}

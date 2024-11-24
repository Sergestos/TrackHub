using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Service.Services.ExerciseServices;
using TrackHub.Service.Services.ExerciseServices.Models;
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
    private readonly IExerciseSearchService _exerciseSearchService;
    private readonly IMapper _mapper;

    public ExerciseController(IExerciseService exerciseService, IExerciseRepository exerciseRepository, IExerciseSearchService exerciseSearchService, IMapper mapper)
    {
        _exerciseService = exerciseService;
        _exerciseRepository = exerciseRepository;
        _exerciseSearchService = exerciseSearchService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ExerciseView), 200)]
    public async Task<IActionResult> GetByIdAsync([FromQuery] string exerciseId, CancellationToken cancellationToken)
    {         
        var result = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, CurrentUserId, cancellationToken);

        return Ok(_mapper.Map<ExerciseView>(result)); 
    }

    [HttpGet]
    [Route("by-date")]
    [ProducesResponseType(typeof(ExerciseView), 200)]
    public IActionResult GetByDateAsync([FromQuery] DateOnly date, CancellationToken cancellationToken)
    {
        var result = _exerciseRepository.GetExerciseByDate(date, CurrentUserId, cancellationToken);

        return Ok(_mapper.Map<ExerciseView>(result));
    }

    [HttpGet]
    [Route("list")]    
    [ProducesResponseType(typeof(IEnumerable<ExerciseListItem>), 200)]
    public async  Task<IActionResult> GetExercisesByDateAsync([FromQuery] int? year, [FromQuery] int? month, CancellationToken cancellationToken)
    {
        var result = await _exerciseSearchService.GetExercisesByDateAsync(year, month, CurrentUserId, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<ExerciseListItem>>(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Exercise), 201)]
    public async Task<IActionResult> PostAsync([FromBody] CreateExerciseModel model, CancellationToken cancellationToken)
    {
        var result = await _exerciseService.CreateExerciseAsync(model, CurrentUserId, cancellationToken);

        return StatusCode(201, result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Exercise), 200)]
    public async Task<IActionResult> PutAsync([FromBody] UpdateExerciseModel model, CancellationToken cancellationToken)
    {
        var result = await _exerciseService.UpdateExerciseAsync(model, CurrentUserId, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]    
    public async Task<IActionResult> DeleteAsync(string exerciseId, CancellationToken cancellationToken)
    {
        await _exerciseService.DeleteExerciseAsync(exerciseId, CurrentUserId, cancellationToken);

        return Ok();
    }

    [HttpDelete]
    [Route("{exerciseId}/records")]
    public async Task<IActionResult> DeleteRecordsAsync([FromRoute] string exerciseId, [FromQuery] string[] recordId, CancellationToken cancellationToken)
    {
        var result = await _exerciseService.DeleteRecordsAsync(exerciseId, recordId, CurrentUserId, cancellationToken);

        return Ok(result);
    }
}

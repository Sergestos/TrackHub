using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Domain.Aggregations;
using TrackHub.Service.Services.AggregationServices;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/aggregations")]
public class AggregationController : TrackHubController
{
    private readonly IAggregationReadService _aggregationReadService;

    public AggregationController(IAggregationReadService aggregationReadService)
    {
        _aggregationReadService = aggregationReadService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ExerciseAggregation), 200)]
    [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> GetExerciseAggregation([FromQuery] DateTime date, CancellationToken cancellationToken)
    {
        var result = await _aggregationReadService.GetExerciseAggregationByDateAsync(CurrentUserId, date, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("range")]
    [ProducesResponseType(typeof(IEnumerable<ExerciseAggregation>), 200)]
    [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> GetExerciseAggregationRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken cancellationToken)
    {
        var result = await _aggregationReadService.GetExerciseAggregationsByDateRangeAsync(CurrentUserId, startDate, endDate, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("songs")]
    [ProducesResponseType(typeof(IEnumerable<SongAggregation>), 200)]
    [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> GetSongAggregations([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] DateTime? date, CancellationToken cancellationToken)
    {
        DateOnly? dateOnly = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;

        var result = await _aggregationReadService.GetSongAggregationsAsync(CurrentUserId, page, pageSize, dateOnly, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("days-trend")]
    [ProducesResponseType(typeof(DaysTrendAggregation), 200)]
    [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> GetRecentPeriodTrendsAgregation(CancellationToken cancellationToken)
    {
        var result = await _aggregationReadService.GetDaysTrendAggregationsAsync(CurrentUserId, cancellationToken);

        return Ok(result);
    }
}

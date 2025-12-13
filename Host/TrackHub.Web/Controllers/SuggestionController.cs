using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Service.Scraper;
using TrackHub.Service.Scraper.Models;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SuggestionsController : TrackHubController
{
    private readonly IScraperFacade _scraperFacade;

    public SuggestionsController(IScraperFacade scraperFacade)
    {
        _scraperFacade = scraperFacade;
    }

    [HttpGet]
    [Route("authors")]
    [ProducesResponseType(typeof(IEnumerable<ScraperSearchResult>), 200)]
    public async Task<IActionResult> GetAuthorsAsync([FromQuery] string pattern, CancellationToken cancellationToken)
    {
        var result = await _scraperFacade.SearchForAuthorsAsync(pattern, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("songs")]
    [ProducesResponseType(typeof(IEnumerable<ScraperSearchResult>), 200)]
    public async Task<IActionResult> GetSongsAsync([FromQuery] string pattern, [FromQuery] string? author, CancellationToken cancellationToken)
    {
        var result = await _scraperFacade.SearchForSongsAsync(pattern, author, cancellationToken);

        return Ok(result);
    }
}

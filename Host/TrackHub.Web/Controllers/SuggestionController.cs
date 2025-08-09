using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Scraper;
using TrackHub.Scraper.Models;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SuggestionController : TrackHubController
{
    private readonly IScraperFacade _scraperFacade;

    public SuggestionController(IScraperFacade scraperFacade)
    {
        _scraperFacade = scraperFacade;
    }

    [HttpGet]
    [Route("authors")]
    [ProducesResponseType(typeof(IEnumerable<ScrapperSearchResult>), 200)]
    public async Task<IActionResult> GetAuthorsAsync([FromQuery] string pattern, CancellationToken cancellationToken)
    {
        var result = await _scraperFacade.SearchForAuthorsAsync(pattern, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("songs")]
    [ProducesResponseType(typeof(IEnumerable<ScrapperSearchResult>), 200)]
    public async Task<IActionResult> GetSongsAsync([FromQuery] string pattern, [FromQuery] string? author, CancellationToken cancellationToken)
    {
        var result = await _scraperFacade.SearchForSongsAsync(pattern, author, cancellationToken);

        return Ok(result);
    }
}

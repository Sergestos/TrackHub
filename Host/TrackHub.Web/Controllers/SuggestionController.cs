using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Searcher;
using TrackHub.Searcher.Models;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SuggestionController : TrackHubController
{
    private readonly ISearcherFacade _searcherFacade;

    public SuggestionController(ISearcherFacade searcherFacade)
    {
        _searcherFacade = searcherFacade;
    }

    [HttpGet]
    [Route("authors")]
    [ProducesResponseType(typeof(IEnumerable<SearchResult>), 200)]
    public async Task<IActionResult> GetAuthorsAsync([FromQuery] string pattern, CancellationToken cancellationToken)
    {
        var result = await _searcherFacade.SearchForAuthorsAsync(pattern, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("songs")]
    [ProducesResponseType(typeof(IEnumerable<SearchResult>), 200)]
    public async Task<IActionResult> GetSongsAsync([FromQuery] string pattern, [FromQuery] string? author, CancellationToken cancellationToken)
    {
        var result = await _searcherFacade.SearchForSongsAsync(pattern, author, cancellationToken);

        return Ok(result);
    }
}

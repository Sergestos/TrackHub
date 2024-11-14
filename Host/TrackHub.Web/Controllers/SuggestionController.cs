using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Crawler;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SuggestionController : TrackHubController
{
    private readonly ICrawlerFacade _crawlerFacade;

    public SuggestionController(ICrawlerFacade crawlerFacade)
    {
        _crawlerFacade = crawlerFacade;
    }

    [HttpGet]
    [Route("authors")]
    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    public async Task<IActionResult> GetAuthorsAsync(string pattern, CancellationToken cancellationToken)
    {
        var result = await _crawlerFacade.SearchForAuthorsAsync(pattern, cancellationToken);

        return Ok(result);
    }
}

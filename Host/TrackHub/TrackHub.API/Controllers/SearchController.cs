using Microsoft.AspNetCore.Mvc;
using TrackHub.Domain;
using TrackHub.Service.Exercises;

namespace TrackHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : Controller
{
    private readonly ISearchable<Song> _searchService;
    private readonly ISearchable<Author> _authorService;
    private readonly ILogger<SearchController> _logger;

    public SearchController(ISearchable<Song> searchService, ISearchable<Author> authorService, ILogger<SearchController> logger)
    {
        _searchService = searchService;
        _authorService = authorService;
        _logger = logger;
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Get(string type, string searchText, CancellationToken cancellationToken)
    {        
        if (type == "song")        
            return Ok(await _searchService.SearchAsync(searchText, cancellationToken));
        
        if (type == "author")        
            return Ok(await _authorService.SearchAsync(searchText, cancellationToken));            
        
        return BadRequest("Unsupported search type.");                
    }
}

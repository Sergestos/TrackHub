/*using Microsoft.AspNetCore.Mvc;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Interfaces;

namespace TrackHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : Controller
{
    private readonly IExerciseSearchProvider _songRepository;    

    public SearchController(IExerciseSearchProvider songRepository)
    {
        _songRepository = songRepository;        
    }

    [HttpGet]
    [Route("song")]
    [ProducesResponseType(typeof(IEnumerable<Song>), 200)]
    public async Task<IActionResult> GetAsync(string searchText, CancellationToken cancellationToken)
    {
        var result = await _songRepository.SearchByTextAsync(searchText,cancellationToken);

        return Ok(result);
    }
}
*/
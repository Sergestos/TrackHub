using Microsoft.AspNetCore.Mvc;
using TrackHub.Web.Controllers;

namespace TrackHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : TrackHubController
{
    public HealthCheckController() { }

    [HttpGet]
    [ProducesResponseType(typeof(string), 200)]
    public IActionResult Get()
    {         
        return Ok("Application works correctly.");
    }
}

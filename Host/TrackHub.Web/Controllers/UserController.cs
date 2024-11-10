using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Service.Services.UserServices;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : TrackHubController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("first-play")]
    [ProducesResponseType(typeof(string), 200)]
    public IActionResult GetByIdAsync()
    {
        var result = _userService.GetUserFirstPlayDateAsync(CurrentUserId);

        return Ok(result);
    }
}

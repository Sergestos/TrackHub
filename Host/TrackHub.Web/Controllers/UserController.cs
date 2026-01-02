using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Service.Services.UserServices;
using TrackHub.Service.Services.UserServices.Models;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : TrackHubController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("current/settings")]
    [ProducesResponseType(typeof(UserSettings), 200)]
    public IActionResult GetSettings()
    {
        var result = _userService.GetUserSettings(CurrentUserId);

        return Ok(result);
    }
}

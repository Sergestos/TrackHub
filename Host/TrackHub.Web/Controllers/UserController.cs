using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Service.User.Features.Queries;
using TrackHub.Service.User.Models;

namespace TrackHub.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : TrackHubController
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Route("current/settings")]
    [ProducesResponseType(typeof(UserSettingsModel), 200)]
    public async Task<IActionResult> GetSettings()
    {
        var result = await _sender.Send(new GetUserSettingsQuery(CurrentUserId));

        return Ok(result);
    }
}

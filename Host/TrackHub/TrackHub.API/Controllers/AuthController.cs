using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TrackHub.Web.Controllers;

public class AuthController : Controller
{
    [AllowAnonymous]
    [HttpGet]
    [Route("/google-login")]
    public IActionResult GoogleSignIn()
    {
        
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/google-response")]
    public async Task<IActionResult> GoogleResponseAsync()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result == null)
            return BadRequest();

        return Ok();
    }

    [Authorize]
    [HttpGet]
    [Route("/logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}

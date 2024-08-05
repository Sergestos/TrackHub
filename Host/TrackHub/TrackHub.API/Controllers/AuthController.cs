using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TrackHub.Web.Models;

namespace TrackHub.Web.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    private IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/google-login")]
    public async Task<GoogleJsonWebSignature.Payload> GoogleSignIn([FromBody] SocialUser socialUser)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["Authentication:Google:ClientId"]! }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(socialUser.IdToken, settings);

            return payload;
        }
        catch (Exception ex)
        {
            //log an exception
            return null;
        }

        /*  var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

          return Challenge(properties, GoogleDefaults.AuthenticationScheme);*/
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

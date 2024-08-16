using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Web.Models;
using TrackHub.Web.Utilities;

namespace TrackHub.Web.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _jwtTokenGenerator = new JwtTokenGenerator(_configuration["Authentication:Jwt:PrivateKey"]!);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("google-login")]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> GoogleSignIn([FromBody] GoogleAuthModel model)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings();
            settings.Audience = new List<string>() { _configuration["Authentication:Google:ClientId"]! };
            
            var googlePayload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
            if (googlePayload != null)
            {
                var user = await GetStoredUser(googlePayload);
                var token = _jwtTokenGenerator.CreateUserAuthToken(user);          

                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid google token.");
            }            
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal error.\n" + ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

        return Ok();
    }

    private async Task<SocialUser> GetStoredUser(GoogleJsonWebSignature.Payload payload)
    {
        var storedUser = _userRepository.GetUserByEmailAsync(payload.Email, CancellationToken.None).Result;
        if (storedUser == null)
        {
            var user = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = payload.Email,
                FullName = $"{payload.GivenName} {payload.FamilyName}",
                PhotoUrl = payload.Picture,
                RegistrationDate = DateTime.UtcNow,
                LastEntranceDate = DateTime.UtcNow
            };

            storedUser = await _userRepository.RegistrateUser(user, CancellationToken.None);
            if (storedUser == null)
            {
                throw new Exception("User registration failed.");
            }
        }
        /*else
        {
            storedUser.LastEntranceDate = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(storedUser, CancellationToken.None);
        }*/

        return new SocialUser()
        {
            Email = storedUser.Email,
            FullName = storedUser.FullName,
            PhotoUrl = storedUser.PhotoUrl
        };
    }
}

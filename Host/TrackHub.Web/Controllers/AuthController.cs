using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using TrackHub.Domain.Entities;
using TrackHub.Service.Services.UserServices;
using TrackHub.Service.Services.UserServices.Models;
using TrackHub.Web.Utilities;

namespace TrackHub.Web.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly IWebHostEnvironment _env;

    public AuthController(
        IConfiguration configuration, 
        IWebHostEnvironment env,
        IUserService userService, 
        IMapper mapper)
    {
        _mapper = mapper;
        _configuration = configuration;
        _env = env;
        _userService = userService;
        _jwtTokenGenerator = new JwtTokenGenerator(_configuration["Authentication:Jwt:PrivateKey"]!);
    }

    [HttpGet]
    [Authorize]
    [Route("validate-token")]
    public IActionResult ValidateToken()
    {
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("google-login")]
    public async Task<IActionResult> GoogleSignIn([FromBody] GoogleAuthTokenModel model, CancellationToken cancellationToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings();
            settings.Audience = new List<string>() { _configuration["Authentication:Google:ClientId"]! };

            var googlePayload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
            if (googlePayload != null)
            {
                var user = await _userService.GetInsertedUserAsync(_mapper.Map<SocialUser>(googlePayload), cancellationToken);
                var jwt = _jwtTokenGenerator.CreateUserAuthToken(user);

                await IssueRefreshToken(user, cancellationToken);

                return Ok(new { user, jwt });
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

    [HttpPost]
    [AllowAnonymous]
    [Route("test-login")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ProducesResponseType(typeof(string), 200)]
    public ActionResult TestSignIn([FromBody] AuthUserModel model)
    {
        var ip = HttpContext.Connection.RemoteIpAddress;
        if (!(ip is not null && IPAddress.IsLoopback(ip)) && !_env.IsDevelopment()) 
            return Forbid();

        try
        {
            var user = _userService.GetUserById(model.UserId);
            if (user is null)
                throw new Exception("User is not found");

            var jwt = _jwtTokenGenerator.CreateUserAuthToken(user);

            return Ok(new { user, jwt });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal error.\n" + ex.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("refresh")]
    public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
    {
        if (!Request.Cookies.TryGetValue("refresh_token", out var token) || string.IsNullOrWhiteSpace(token))
            return Unauthorized();

        var (userId, sessionId, secret) = RefreshTokenHelper.UnpackRefreshToken(token)!.Value;
        var user = _userService.GetUserById(userId);

        if (user!.LoginSession == null || user.LoginSession.SessionId != sessionId)
            return Unauthorized();

        if (user.LoginSession.ExpiresAt <= DateTime.UtcNow)
            return Unauthorized();

        var jwt = _jwtTokenGenerator.CreateUserAuthToken(user);

        await IssueRefreshToken(user, cancellationToken);

        return Ok(new { user, jwt });
    }

    [HttpPost]
    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        if (!Request.Cookies.TryGetValue("refresh_token", out var token) || string.IsNullOrWhiteSpace(token))
        {
            return NoContent();
        }

        var parsed = RefreshTokenHelper.UnpackRefreshToken(token);
        if (parsed is null)
        {
            ClearRefreshCookie(Response);
            return NoContent();
        }

        var (userId, sessionId, secret) = parsed.Value;

        var user = _userService.GetUserById(userId)!;
        user.LoginSession = null;
        await _userService.UpdateUserAsync(user, cancellationToken);

        ClearRefreshCookie(Response);

        return NoContent();
    }

    private async Task IssueRefreshToken(User user, CancellationToken cancellationToken)
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        var secret = Convert.ToBase64String(bytes);
        var expiresAt = DateTime.UtcNow.AddDays(30);

        var sessionId = Guid.NewGuid().ToString();
        var refreshToken = RefreshTokenHelper.PackRefreshToken(user.UserId, sessionId, secret);

        user.LoginSession = new LoginSession()
        {
            SessionId = sessionId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt,
        };

        await _userService.UpdateUserAsync(user, cancellationToken);

        HttpContext.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = _env.IsProduction(),
            SameSite = SameSiteMode.Lax,
            Expires = expiresAt,
            Path = "/api/auth/refresh"
        });
    }

    private void ClearRefreshCookie(HttpResponse response)
    {
        response.Cookies.Append("refresh_token", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddDays(-1),
            Path = "/api/auth/refresh"
        });
    }
}

public record GoogleAuthTokenModel
{
    public required string IdToken { get; set; }
}

public record AuthUserModel
{
    public required string UserId { get; set; }
}
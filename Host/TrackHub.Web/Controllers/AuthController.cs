using System.Net;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [ProducesResponseType(typeof(string), 200)]
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
                var token = _jwtTokenGenerator.CreateUserAuthToken(user);

                return Ok(new { user, token });
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

            var token = _jwtTokenGenerator.CreateUserAuthToken(user);

            return Ok(new { user, token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal error.\n" + ex.Message);
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
}
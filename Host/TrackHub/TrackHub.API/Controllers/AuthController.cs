using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackHub.Service.UserServices;
using TrackHub.Service.UserServices.Models;
using TrackHub.Web.Utilities;

namespace TrackHub.Web.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(IConfiguration configuration, IUserService userService, IMapper mapper)
    {
        _mapper = mapper;
        _configuration = configuration;
        _userService = userService;
        _jwtTokenGenerator = new JwtTokenGenerator(_configuration["Authentication:Jwt:PrivateKey"]!);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("google-login")]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> GoogleSignIn([FromBody] GoogleAuthToken model, CancellationToken cancellationToken)
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
}

public class GoogleAuthToken
{
    public required string IdToken { get; set; }
}

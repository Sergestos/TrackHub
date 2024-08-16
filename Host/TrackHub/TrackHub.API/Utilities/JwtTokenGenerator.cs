using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackHub.Service.UserServices.Models;

namespace TrackHub.Web.Utilities;

internal sealed class JwtTokenGenerator
{
    private readonly SymmetricSecurityKey _key;

    public JwtTokenGenerator(string signingKey)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(signingKey);

        _key = new SymmetricSecurityKey(key);
    }

    public string CreateUserAuthToken(SocialUser socialUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(socialUser),
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    private ClaimsIdentity GenerateClaims(SocialUser socialUser)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Email, socialUser.Email));
        claims.AddClaim(new Claim(ClaimTypes.Name, socialUser.FullName));
        claims.AddClaim(new Claim(ClaimTypes.Uri, socialUser.PhotoUrl));

        return claims;
    }
}

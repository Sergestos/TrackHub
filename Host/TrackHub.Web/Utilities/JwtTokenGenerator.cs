using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackHub.Domain.Entities;

namespace TrackHub.Web.Utilities;

internal sealed class JwtTokenGenerator
{
    private readonly SymmetricSecurityKey _key;

    public JwtTokenGenerator(string signingKey)
    {
        var key = Encoding.ASCII.GetBytes(signingKey);

        _key = new SymmetricSecurityKey(key);
    }

    public string CreateUserAuthToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Sid, user.UserId));
        claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claims.AddClaim(new Claim(ClaimTypes.Name, user.FullName));
        claims.AddClaim(new Claim(ClaimTypes.Uri, user.PhotoUrl));

        return claims;
    }
}

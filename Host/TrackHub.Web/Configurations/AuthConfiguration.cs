using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TrackHub.Web.Configurations;

public static class AuthConfiguration
{
    public static void AddAuthServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(options =>
        {
     /*       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;*/
        });
    /*    .AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:PrivateKey"]!)
                ),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

        })*/
      /*  .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication:Google:ClientId"]!;
            googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
        });*/
    }
}

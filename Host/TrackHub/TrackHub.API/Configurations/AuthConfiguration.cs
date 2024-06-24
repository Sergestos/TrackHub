using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;

namespace TrackHub.Web.Configurations;

public static class AuthConfiguration
{
    public static void AddAuthServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/auth/google-login";
            options.LogoutPath = "/auth/logout";
        })
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication:Google:ClientId"]!;
            googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
            googleOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            googleOptions.CallbackPath = new PathString("/auth/google-response");
            googleOptions.ClaimsIssuer = "https://www.googleapis.com/oauth2/v1/certs";
            googleOptions.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
            googleOptions.TokenEndpoint = "https://oauth2.googleapis.com/token";
            googleOptions.SaveTokens = true;
        });
    }
}

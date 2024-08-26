namespace TrackHub.Web.Configurations;

public static class CorsPolicyConfiguration
{
    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(
                        "http://localhost:4200",
                        "http://localhost:5044",
                        "https://localhost:7012",
                        "https://accounts.google.com");
            });
        });  
    }
}

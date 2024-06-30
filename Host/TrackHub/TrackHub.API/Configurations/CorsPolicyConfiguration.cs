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
                    .WithOrigins("http://localhost:7012", "http://localhost:4200", "https://accounts.google.com")
                    .AllowCredentials();
            });
        });  
    }
}

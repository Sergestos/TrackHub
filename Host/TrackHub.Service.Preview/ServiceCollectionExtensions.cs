using Microsoft.Extensions.DependencyInjection;
using TrackHub.Application.Preview;

public static class ServiceCollectionExtensions
{
    public static void AddPreviewServices(this IServiceCollection services)
    {
        services.AddScoped<IPreviewService, PreviewService>();
    }
}

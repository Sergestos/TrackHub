using Microsoft.Extensions.DependencyInjection;

namespace TrackHub.Application.Service.Preview;

public static class ServiceCollectionExtensions
{
    public static void AddPreviewServices(this IServiceCollection services)
    {
        services.AddScoped<IPreviewService, PreviewService>();
    }
}

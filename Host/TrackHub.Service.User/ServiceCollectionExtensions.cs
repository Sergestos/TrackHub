using Microsoft.Extensions.DependencyInjection;
using TrackHub.Application.Service.User.Queries;

namespace TrackHub.Application.Service.User;

public static class ServiceCollectionExtensions
{
    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetUserSettingsHandler).Assembly);
        });
    }
}

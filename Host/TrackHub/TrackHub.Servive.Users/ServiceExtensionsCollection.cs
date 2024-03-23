using Microsoft.Extensions.DependencyInjection;
using TrackHub.Service.Users.Implementation;

namespace TrackHub.Service.Users;

public static class ServiceExtensionsCollection
{
    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}

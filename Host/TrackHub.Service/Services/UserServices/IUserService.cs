using TrackHub.Domain.Entities;
using TrackHub.Service.Services.UserServices.Models;

namespace TrackHub.Service.Services.UserServices;

public interface IUserService
{
    Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);

    Task<User> GetInsertedUserAsync(SocialUser userModel, CancellationToken cancellationToken);

    User? GetUserById(string userId);

    UserSettings GetUserSettings(string userId);
}

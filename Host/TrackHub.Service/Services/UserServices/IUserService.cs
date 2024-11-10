using TrackHub.Domain.Entities;
using TrackHub.Service.Services.UserServices.Models;

namespace TrackHub.Service.Services.UserServices;

public interface IUserService
{
    Task<User> GetInsertedUserAsync(SocialUser userModel, CancellationToken cancellationToken);

    DateTimeOffset GetUserFirstPlayDateAsync(string userId);
}

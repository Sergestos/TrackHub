using TrackHub.Domain.Entities;
using TrackHub.Service.UserServices.Models;

namespace TrackHub.Service.UserServices;

public interface IUserService
{
    Task<User> GetInsertedUserAsync(SocialUser userModel, CancellationToken cancellationToken);
}

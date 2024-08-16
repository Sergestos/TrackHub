using TrackHub.Service.UserServices.Models;

namespace TrackHub.Service.UserServices;

public interface IUserService
{
    Task<SocialUser> GetInsertedUserAsync(SocialUser userModel, CancellationToken cancellationToken);
}

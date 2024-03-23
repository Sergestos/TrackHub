using TrackHub.Service.Users.Models;

namespace TrackHub.Service.Users;

public interface IUserService
{
    Task<UserViewModel> RegistrateAsync(UserCreateModel user);
}


using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Service.Services.UserServices.Models;

namespace TrackHub.Service.Services.UserServices;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetInsertedUserAsync(SocialUser userModel, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetUserByEmail(userModel.Email);
        if (user == null)
        {
            var newUser = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = userModel.Email,
                FullName = userModel.FullName,
                PhotoUrl = userModel.PhotoUrl,
                RegistrationDate = DateTime.UtcNow,
                LastEntranceDate = DateTime.UtcNow
            };

            user = await _userRepository.UpsertAsync(newUser, cancellationToken);
        }
        else
        {
            user.LastEntranceDate = DateTime.UtcNow;
            await _userRepository.UpsertAsync(user, cancellationToken);
        }

        return user!;
    }

    public User? GetUserById(string userId)
    {
        return _userRepository.GetUserById(userId);
    }

    public UserSettings GetUserSettings(string userId)
    {
        var userSettings = new UserSettings();

        User user = _userRepository.GetUserById(userId)!;
        if (user.FirstPlayDate.HasValue)
            userSettings.FirstPlayDate = user.FirstPlayDate!.Value;
        else 
            userSettings.FirstPlayDate = DateTimeOffset.UtcNow;

        return userSettings;
    }
}

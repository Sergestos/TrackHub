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
        User? user = await _userRepository.GetUserByEmailAsync(userModel.Email, cancellationToken);
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

    public async Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByIdAsync(userId, cancellationToken);
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var updatedUser = await _userRepository.UpsertAsync(user, cancellationToken);

        return updatedUser!;
    }
}

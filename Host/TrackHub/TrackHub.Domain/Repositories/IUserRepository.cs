using TrackHub.Domain.Entities;

namespace TrackHub.Domain.Repositories;

public interface IUserRepository
{
    User? GetUserById(string userId);

    User? GetUserByEmail(string userEmail);

    Task<User?> UpsertAsync(User user, CancellationToken cancellationToken);
}

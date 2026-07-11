using TrackHub.Domain.Entities;

namespace TrackHub.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken);

    Task<User?> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken);

    Task<User?> UpsertAsync(User user, CancellationToken cancellationToken);
}

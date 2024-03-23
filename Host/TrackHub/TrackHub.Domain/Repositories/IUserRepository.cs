using TrackHub.Domain.Entities;

namespace TrackHub.Domain.Repositories;

public interface IUserRepository
{
    Task<User> Registrate(User user);
}

using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal sealed class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository() : base() { }

    public Task<User> Registrate(User user)
    {
        throw new NotImplementedException();
    }
}

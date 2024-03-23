using Cassandra;
using TrackHub.Data.Cassandra;
using TrackHub.Service.Users.Models;

namespace TrackHub.Service.Users.Implementation;

internal sealed class UserService : IUserService
{
    private readonly ISession _dbSession;

    public UserService()
    {
        _dbSession = CassandraBuilder.Session!;
    }

    public Task<UserViewModel> RegistrateAsync(UserCreateModel user)
    {
        throw new NotImplementedException();
    }
}

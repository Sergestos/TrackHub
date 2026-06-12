using Microsoft.Azure.Cosmos;
using TrackHub.CosmosDb;
using TrackHub.Domain.Repositories;
using DomainUser = TrackHub.Domain.Entities.User;

namespace TrackHub.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private const string UserContainerType = "user";

    private readonly Container _container;

    public UserRepository(ICosmosDbContext context)
    {
        _container = context.GetContainer(UserContainerType);
    }

    public DomainUser? GetUserById(string userId)
    {
        var result = _container.GetItemLinqQueryable<DomainUser>()
            .Where(x => x.UserId == userId)
            .FirstOrDefault();

        return result;
    }

    public DomainUser? GetUserByEmail(string userEmail)
    {
        var result = _container.GetItemLinqQueryable<DomainUser>()
            .Where(x => x.Email == userEmail)
            .FirstOrDefault();

        return result;
    }

    public async Task<DomainUser?> UpsertAsync(DomainUser user, CancellationToken cancellationToken)
    {
        ItemResponse<DomainUser>? response = null;

        try
        {
            response = await _container.UpsertItemAsync(user, new PartitionKey(user.Email), null, cancellationToken);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) { }

        return response?.Resource;
    }
}

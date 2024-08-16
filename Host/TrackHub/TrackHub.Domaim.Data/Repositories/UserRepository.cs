using Microsoft.Azure.Cosmos;
using TrackHub.CosmosDb;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal class UserRepository : IUserRepository
{
    private const string UserContainerType = "user";

    private readonly Container _container;

    public UserRepository(ICosmosDbContext context)
    {
        _container = context.GetContainer(UserContainerType);
    }

    public async Task<Entities.User?> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken)
    {
        ItemResponse<Entities.User>? response = null;

        try
        {
            response = await _container.ReadItemAsync<Entities.User>(userEmail, new PartitionKey(userEmail), null, cancellationToken);            
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) { }        

        return response?.Resource;
    }

    public async Task<Entities.User?> RegistrateUser(Entities.User user, CancellationToken cancellationToken)
    {
        ItemResponse<Entities.User>? response = null;

        try
        {
            response = await _container.UpsertItemAsync(user, new PartitionKey(user.Email), null, cancellationToken);
        }
        catch (CosmosException ex) when(ex.StatusCode == System.Net.HttpStatusCode.NotFound) { }        

        return response?.Resource;
    }

    public Task<Entities.User> UpdateUserAsync(Entities.User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

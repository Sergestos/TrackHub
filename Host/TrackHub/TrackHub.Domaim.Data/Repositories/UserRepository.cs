using Microsoft.Azure.Cosmos;
using TrackHub.CosmosDb;
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

    public Entities.User? GetUserByEmail(string userEmail)
    {
        var result = _container.GetItemLinqQueryable<Entities.User>()
            .Where(x => x.Email == userEmail)
            .FirstOrDefault();

        return result;
    }

    public async Task<Entities.User?> UpsertAsync(Entities.User user, CancellationToken cancellationToken)
    {
        ItemResponse<Entities.User>? response = null;

        try
        {
            response = await _container.UpsertItemAsync(user, new PartitionKey(user.Email), null, cancellationToken);
        }
        catch (CosmosException ex) when(ex.StatusCode == System.Net.HttpStatusCode.NotFound) { }        

        return response?.Resource;
    }
}

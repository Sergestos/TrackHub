using Microsoft.Azure.Cosmos;

namespace TrackHub.Domain.Data.Repositories;

internal abstract class CosmosContainerIterator<T>
{
    protected Container? Container;

    protected async Task<IEnumerable<T>> IterateFeedAsync(QueryDefinition queryDefinition)
    {
        using FeedIterator<T> feed = Container!.GetItemQueryIterator<T>(queryDefinition);        

        var result = new List<T>();
        while (feed.HasMoreResults)
        {
            FeedResponse<T> response = await feed.ReadNextAsync();

            foreach (var item in response)
            {
                result.Add(item);
            }
        }

        return result;
    }
}

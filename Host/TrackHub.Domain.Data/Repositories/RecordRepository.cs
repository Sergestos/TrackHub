using Microsoft.Azure.Cosmos;
using TrackHub.CosmosDb;
using TrackHub.Domain.Repositories;

namespace TrackHub.Domain.Data.Repositories;

internal class RecordRepository : CosmosContainerIterator<string>, IRecordRepository
{
    private const string ExerciseContainerType = "exercise";

    public RecordRepository(ICosmosDbContext context)
    {
        Container = context.GetContainer(ExerciseContainerType);
    }

    public async Task<IEnumerable<string>> SearchAuthorsByNameAsync(string pattern, int searchSize, CancellationToken cancellationToken)
    {
        string query = @"
            SELECT DISTINCT TOP @top
               VALUE r.author
            FROM r IN
               e.records
            WHERE 
               r.record_type IN('Warmup', 'Song') 
               AND r.author LIKE @pattern";

        QueryDefinition queryDefinition = new QueryDefinition(query)
            .WithParameter("@pattern", pattern + "%")
            .WithParameter("@top", searchSize);        

        return await IterateFeedAsync(queryDefinition);        
    }

    public async Task<IEnumerable<string>> SearchSongsByNameAsync(string pattern, int searchSize, CancellationToken cancellationToken)
    {
        string query = @"
            SELECT DISTINCT TOP @top                
               VALUE r.name
            FROM r IN
               e.records
            WHERE 
               r.record_type IN('Warmup', 'Song') 
               AND r.name LIKE @pattern";

        QueryDefinition queryDefinition = new QueryDefinition(query)
            .WithParameter("@pattern", pattern + "%")
            .WithParameter("@top", searchSize);

        return await IterateFeedAsync(queryDefinition);
    }
}

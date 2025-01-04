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

    public async Task<IEnumerable<string>> SearchAuthorsByNameAsync(string pattern, int searchSize, string[]? excludeList, CancellationToken cancellationToken)
    {
        string query = @"
            SELECT DISTINCT TOP @top
               VALUE r.author
            FROM r IN
               e.records
            WHERE 
               r.record_type IN('Warmup', 'Song') 
               AND r.author LIKE @pattern
               AND NOT ARRAY_CONTAINS(@excludeList, r.author)";

        string excludeListParam = excludeList != null ? "[" + string.Join(", ", excludeList) + "]": "";

        QueryDefinition queryDefinition = new QueryDefinition(query)
            .WithParameter("@pattern", pattern + "%")
            .WithParameter("@top", searchSize)
            .WithParameter("@excludeList", excludeListParam);

        return await IterateFeedAsync(queryDefinition);        
    }

    public async Task<IEnumerable<string>> SearchSongsByNameAsync(string pattern, int searchSize, string[]? excludeList, CancellationToken cancellationToken)
    {
        string query = @"
            SELECT DISTINCT TOP @top                
               VALUE r.name
            FROM r IN
               e.records
            WHERE 
               r.record_type IN('Warmup', 'Song') 
               AND r.name LIKE @pattern
               AND NOT ARRAY_CONTAINS(@excludeList, r.name)";

        string excludeListParam = excludeList != null ? "[" + string.Join(", ", excludeList) + "]" : "";

        QueryDefinition queryDefinition = new QueryDefinition(query)
            .WithParameter("@pattern", pattern + "%")
            .WithParameter("@top", searchSize)
            .WithParameter("@excludeList", excludeListParam);

        return await IterateFeedAsync(queryDefinition);
    }
}

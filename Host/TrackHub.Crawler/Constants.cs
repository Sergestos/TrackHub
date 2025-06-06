namespace TrackHub.Scraper;

internal static class Constants
{
    internal static int MinimalDbResultThreshold  = 3;
    internal static int MinimalSearchPatternLength = 3;
    internal static int MaximumSearchResultLength  = 5;

   /* protected abstract Task<IEnumerable<string>> SearchFromCacheAsync(string pattern, IEnumerable<string>? excludeItems, CancellationToken cancellation);

    protected abstract Task<IEnumerable<string>> SearchFromDatabaseAsync(string pattern, IEnumerable<string>? excludeItems, CancellationToken cancellation);

    protected abstract Task<IEnumerable<string>> SearchFromAiAsync(string pattern, IEnumerable<string>? excludeItems, CancellationToken cancellation);

    public async Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string pattern, CancellationToken cancellation)
    {
        IList<ScrapperSearchResult> result = new List<ScrapperSearchResult>();

        var cacheResult = await SearchFromCacheAsync(pattern, null, cancellation);
        foreach (var item in cacheResult)
            result.Add(ScrapperSearchResultBuilder.FromCache(item));
        if (result.Count >= MaximumSearchResultLength)
            return result;

        var dbResult = await SearchFromDatabaseAsync(pattern, result.Select(x => x.Result), cancellation);
        foreach (var item in cacheResult)
            result.Add(ScrapperSearchResultBuilder.FromDateBase(item));
        if (result.Count >= MaximumSearchResultLength)
            return result;

        var aiResult = await SearchFromAiAsync(pattern, result.Select(x => x.Result), cancellation);
        foreach (var item in cacheResult)
            result.Add(ScrapperSearchResultBuilder.FromAi(item));

        return result;
    }*/
}

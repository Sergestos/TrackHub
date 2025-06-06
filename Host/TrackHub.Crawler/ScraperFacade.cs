using TrackHub.Scraper.Models;
using TrackHub.Scraper.Searchers.Authors;
using TrackHub.Scraper.Searchers.Song;

namespace TrackHub.Scraper;

internal class ScraperFacade : IScraperFacade
{
    private const string CacheSetIdentifier = "author";
    private const int MaximumSearchResultLength = 5;

    private readonly IAuthorSearcher _authorSearcher;
    private readonly ISongSearcher _songSearcher;
    private readonly IScraperCache _suggestionCache;

    public ScraperFacade(IAuthorSearcher authorSearcher, ISongSearcher songSearcher, IScraperCache suggestionCache)
    {
        _authorSearcher = authorSearcher;
        _songSearcher = songSearcher;
        _suggestionCache = suggestionCache;
    }

    public async Task<IEnumerable<ScrapperSearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken)
    {
        var cachedResults = _suggestionCache.Get(new CacheKey(CacheSetIdentifier, pattern));
        if (cachedResults == null || cachedResults.Length < MaximumSearchResultLength)
        {
            int leftoverSize = cachedResults == null ? MaximumSearchResultLength : MaximumSearchResultLength - cachedResults!.Length;
            var searcherResult = await _authorSearcher.SearchAsync(pattern, cancellationToken);

            if (searcherResult.Any())
            {
                _suggestionCache.Add(new CacheItem()
                {
                    Key = new CacheKey(CacheSetIdentifier, pattern),
                    Values = searcherResult.Select(x => x.Result).ToArray()
                });
            }                

            return cachedResults == null ? searcherResult :
                 cachedResults
                    .Select(ScrapperSearchResultBuilder.FromCache)
                    .Union(searcherResult);
        }
        else
        {
            return cachedResults.Select(ScrapperSearchResultBuilder.FromCache).ToList();
        }
    }

    public async Task<IEnumerable<ScrapperSearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(author))
        {
            return await _songSearcher.SearchAsync(pattern, author, MaximumSearchResultLength, cancellationToken);
        }
        else
        {
            return await  _songSearcher.SearchAsync(pattern, MaximumSearchResultLength, cancellationToken);
        }
    }    
}

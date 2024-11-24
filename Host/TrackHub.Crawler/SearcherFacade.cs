using TrackHub.Seacher.Models;
using TrackHub.Searcher.Models;
using TrackHub.Searcher.Searchers.Authors;
using TrackHub.Searcher.Searchers.Song;

namespace TrackHub.Searcher;

internal class SearcherFacade : ISearcherFacade
{
    private int MaximumSearchResultLength { get; set; } = 5;

    private readonly IAuthorSearcher _authorSearcher;
    private readonly ISongSearcher _songSearcher;
    private readonly ISuggestionCache _suggestionCache;

    public SearcherFacade(IAuthorSearcher authorSearcher, ISongSearcher songSearcher, ISuggestionCache suggestionCache)
    {
        _authorSearcher = authorSearcher;
        _songSearcher = songSearcher;
        _suggestionCache = suggestionCache;
    }
    public async Task<IEnumerable<SearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken)
    {
        string cacheSetIdentifier = "author";

        var cachedResults = _suggestionCache.Get(new CacheKey(cacheSetIdentifier, pattern));
        if (cachedResults == null || cachedResults.Length < MaximumSearchResultLength)
        {
            int leftoverSize = cachedResults == null ? MaximumSearchResultLength : MaximumSearchResultLength - cachedResults!.Length;
            var searcherResult = await _authorSearcher.SearchAsync(pattern, leftoverSize, cancellationToken);

            if (searcherResult.Any())
            {
                _suggestionCache.Add(new CacheItem()
                {
                    Key = new CacheKey(cacheSetIdentifier, pattern),
                    Values = searcherResult.Select(x => x.Result).ToArray()
                });
            }                

            return cachedResults == null ? searcherResult :
                 cachedResults
                    .Select(SearchResultBuilder.FromCache)
                    .Union(searcherResult);
        }
        else
        {
            return cachedResults.Select(SearchResultBuilder.FromCache).ToList();
        }
    }

    public async Task<IEnumerable<SearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken)
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

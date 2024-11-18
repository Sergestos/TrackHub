using TrackHub.Searcher.Models;
using TrackHub.Searcher.Searchers.Authors;
using TrackHub.Searcher.Searchers.Song;

namespace TrackHub.Searcher;

internal class SearcherFacade : ISearcherFacade
{
    private readonly IAuthorSearcher _authorSearcher;
    private readonly ISongSearcher _songSearcher;

    public SearcherFacade(IAuthorSearcher authorSearcher, ISongSearcher songSearcher)
    {
        _authorSearcher = authorSearcher;
        _songSearcher = songSearcher;
    }
    public Task<IEnumerable<SearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken)
    {
        var result = _authorSearcher.SearchAsync(pattern, cancellationToken);

        return result;
    }

    public async Task<IEnumerable<SearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(author))
        {
            return await _songSearcher.SearchAsync(pattern, author, cancellationToken);
        }
        else
        {
            return await  _songSearcher.SearchAsync(pattern, cancellationToken);
        }
    }
}

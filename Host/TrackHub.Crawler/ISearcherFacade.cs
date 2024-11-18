using TrackHub.Searcher.Models;

namespace TrackHub.Searcher;

public interface ISearcherFacade
{
    Task<IEnumerable<SearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken);

    Task<IEnumerable<SearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken);
}

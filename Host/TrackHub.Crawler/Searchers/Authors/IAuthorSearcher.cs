using TrackHub.Searcher.Models;

namespace TrackHub.Searcher.Searchers.Authors;

public interface IAuthorSearcher
{
    Task<IEnumerable<SearchResult>> SearchAsync(string authorName, int resultSize, CancellationToken cancellationToken);
}

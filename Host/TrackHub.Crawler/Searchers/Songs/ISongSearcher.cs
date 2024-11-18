using TrackHub.Searcher.Models;

namespace TrackHub.Searcher.Searchers.Song;

public interface ISongSearcher
{
    Task<IEnumerable<SearchResult>> SearchAsync(string pattern, string authorName, CancellationToken cancellationToken);

    Task<IEnumerable<SearchResult>> SearchAsync(string pattern, CancellationToken cancellationToken);    
}

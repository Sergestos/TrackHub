using TrackHub.Scraper.Models;

namespace TrackHub.Scraper.Searchers.Song;

public interface ISongSearcher
{
    Task<IEnumerable<SearchResult>> SearchAsync(string pattern, string authorName, int resultSize, CancellationToken cancellationToken);

    Task<IEnumerable<SearchResult>> SearchAsync(string pattern, int resultSize, CancellationToken cancellationToken);    
}

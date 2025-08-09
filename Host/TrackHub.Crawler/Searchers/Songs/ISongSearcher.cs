using TrackHub.Scraper.Models;

namespace TrackHub.Scraper.Searchers.Song;

public interface ISongSearcher
{
    Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string pattern, string authorName, int resultSize, CancellationToken cancellationToken);

    Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string pattern, int resultSize, CancellationToken cancellationToken);    
}

using TrackHub.Service.Scraper.Models;

namespace TrackHub.Service.Scraper.Searchers.Song;

public interface ISongSearcher
{
    Task<IEnumerable<ScraperSearchResult>> SearchAsync(string pattern, int resultSize, CancellationToken cancellationToken);    
}

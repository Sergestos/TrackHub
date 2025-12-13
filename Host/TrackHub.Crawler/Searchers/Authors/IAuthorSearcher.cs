using TrackHub.Service.Scraper.Models;

namespace TrackHub.Service.Scraper.Searchers.Authors;

public interface IAuthorSearcher
{
    Task<IEnumerable<ScraperSearchResult>> SearchAsync(string authorName, CancellationToken cancellationToken);
}

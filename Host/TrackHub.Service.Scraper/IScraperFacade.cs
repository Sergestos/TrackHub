using TrackHub.Service.Scraper.Models;

namespace TrackHub.Service.Scraper;

public interface IScraperFacade
{
    Task<IEnumerable<ScraperSearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken);

    Task<IEnumerable<ScraperSearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken);
}

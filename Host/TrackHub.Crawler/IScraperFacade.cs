using TrackHub.Scraper.Models;

namespace TrackHub.Scraper;

public interface IScraperFacade
{
    Task<IEnumerable<SearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken);

    Task<IEnumerable<SearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken);
}

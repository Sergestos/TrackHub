using TrackHub.Service.Scrapper.Models;

namespace TrackHub.Service.Scrapper;

public interface IScraperFacade
{
    Task<IEnumerable<ScrapperSearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken);

    Task<IEnumerable<ScrapperSearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken);
}

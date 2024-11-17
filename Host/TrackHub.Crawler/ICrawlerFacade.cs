using TrackHub.Crawler.Models;

namespace TrackHub.Crawler;

public interface ICrawlerFacade
{
    Task<IEnumerable<SearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken);
}

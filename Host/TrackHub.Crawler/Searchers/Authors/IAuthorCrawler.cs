using TrackHub.Crawler.Models;

namespace TrackHub.Crawler.Searchers.Authors;

public interface IAuthorCrawler
{
    Task<IEnumerable<SearchResult>> SearchAsync(string authorName, CancellationToken cancellationToken);
}

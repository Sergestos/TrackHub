using TrackHub.Scraper.Models;

namespace TrackHub.Scraper.Searchers.Authors;

public interface IAuthorSearcher
{
    Task<IEnumerable<SearchResult>> SearchAsync(string authorName, int resultSize, CancellationToken cancellationToken);
}

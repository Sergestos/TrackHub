using TrackHub.Scraper.Models;

namespace TrackHub.Scraper.Searchers.Authors;

public interface IAuthorSearcher
{
    Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string authorName, int resultSize, string[]? excludeList, CancellationToken cancellationToken);
}

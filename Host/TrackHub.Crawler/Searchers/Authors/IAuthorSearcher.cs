using TrackHub.Service.Scrapper.Models;

namespace TrackHub.Service.Scrapper.Searchers.Authors;

public interface IAuthorSearcher
{
    Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string authorName, CancellationToken cancellationToken);
}

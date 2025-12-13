using TrackHub.Service.Scrapper.Models;

namespace TrackHub.Service.Scrapper.Searchers.Song;

public interface ISongSearcher
{
    Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string pattern, string authorName, int resultSize, CancellationToken cancellationToken);

    Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string pattern, int resultSize, CancellationToken cancellationToken);    
}

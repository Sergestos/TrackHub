using TrackHub.Service.Scraper.Models;
using TrackHub.Service.Scraper.Searchers.Authors;
using TrackHub.Service.Scraper.Searchers.Song;

namespace TrackHub.Service.Scraper;

internal class ScraperFacade : IScraperFacade
{
    private const int MaximumSearchResultLength = 5;

    private readonly IAuthorSearcher _authorSearcher;
    private readonly ISongSearcher _songSearcher;

    public ScraperFacade(IAuthorSearcher authorSearcher, ISongSearcher songSearcher)
    {
        _authorSearcher = authorSearcher;
        _songSearcher = songSearcher;
    }

    public async Task<IEnumerable<ScraperSearchResult>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken)
    {
        return await _authorSearcher.SearchAsync(pattern, cancellationToken);
    }

    public async Task<IEnumerable<ScraperSearchResult>> SearchForSongsAsync(string pattern, string? author, CancellationToken cancellationToken)
    {
        return await  _songSearcher.SearchAsync(pattern, MaximumSearchResultLength, cancellationToken);      
    }    
}

using TrackHub.Crawler.Searchers.Authors;

namespace TrackHub.Crawler;

internal class CrawlerFacade : ICrawlerFacade
{
    private readonly IAuthorCrawler _authorCrawler;

    public CrawlerFacade(IAuthorCrawler authorCrawler)
    {
        _authorCrawler = authorCrawler;
    }
    public Task<IEnumerable<string>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken)
    {
        var result = _authorCrawler.SearchAsync(pattern, cancellationToken);

        return result;
    }
}

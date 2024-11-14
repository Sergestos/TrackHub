namespace TrackHub.Crawler.Searchers.Authors;

public interface IAuthorCrawler
{
    Task<IEnumerable<string>> SearchAsync(string authorName, CancellationToken cancellationToken);
}

namespace TrackHub.Crawler;

public interface ICrawlerFacade
{
    Task<IEnumerable<string>> SearchForAuthorsAsync(string pattern, CancellationToken cancellationToken);
}

namespace TrackHub.Scraper.Models;

public class SearchResult
{
    public required string Result { get; set; }

    public required ResultSource Source { get; set; }
}

public static class SearchResultBuilder
{
    public static SearchResult FromDateBase(string result) => new SearchResult() { Result = result, Source = ResultSource.DateBase };

    public static SearchResult FromAi(string result) => new SearchResult() { Result = result, Source = ResultSource.Ai };

    public static SearchResult FromCache(string result) => new SearchResult() { Result = result, Source = ResultSource.Cache };
}

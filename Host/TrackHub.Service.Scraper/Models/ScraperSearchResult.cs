namespace TrackHub.Service.Scraper.Models;

public class ScraperSearchResult
{
    public required string Result { get; set; }

    public required ResultSource Source { get; set; }
}

public static class ScraperSearchResultBuilder
{
    public static ScraperSearchResult FromDateBase(string result) => new ScraperSearchResult() { Result = result, Source = ResultSource.DateBase };

    public static ScraperSearchResult FromAi(string result) => new ScraperSearchResult() { Result = result, Source = ResultSource.Ai };

    public static ScraperSearchResult FromCache(string result) => new ScraperSearchResult() { Result = result, Source = ResultSource.Cache };
}

namespace TrackHub.Service.Scrapper.Models;

public class ScrapperSearchResult
{
    public required string Result { get; set; }

    public required ResultSource Source { get; set; }
}

public static class ScrapperSearchResultBuilder
{
    public static ScrapperSearchResult FromDateBase(string result) => new ScrapperSearchResult() { Result = result, Source = ResultSource.DateBase };

    public static ScrapperSearchResult FromAi(string result) => new ScrapperSearchResult() { Result = result, Source = ResultSource.Ai };

    public static ScrapperSearchResult FromCache(string result) => new ScrapperSearchResult() { Result = result, Source = ResultSource.Cache };
}

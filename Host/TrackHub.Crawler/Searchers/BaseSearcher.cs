namespace TrackHub.Searcher.Searchers;

internal abstract class BaseSearcher
{
    protected int MinimalDbResultThreshold { get; set; } = 3;
    protected int MinimalSearchPatternLength { get; set; } = 3;
    protected int MaximumSearchResultLength { get; set; } = 5;

    protected IEnumerable<string> PolishAiResponse(IEnumerable<string> aiResponse, IEnumerable<string> dbResult)
    {
        return aiResponse.Where(x => !dbResult.Contains(x));
    }

    protected string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}

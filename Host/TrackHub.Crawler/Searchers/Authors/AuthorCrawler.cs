
using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Crawler.Models;
using TrackHub.Domain.Repositories;

namespace TrackHub.Crawler.Searchers.Authors;

internal class AuthorCrawler : IAuthorCrawler
{
    private const int MinimalDbResultThreshold = 3;
    private const int MinimalSearchPatternLength = 3;
    private const int MaximumSearchResultLength = 5;

    private readonly IRecordRepository _recordRepository;
    private readonly IAiMusicCrawler _aiMusicCrawler;

    public AuthorCrawler(IRecordRepository recordRepository, IAiMusicCrawler aiMusicCrawler)
    {
        _recordRepository = recordRepository;
        _aiMusicCrawler = aiMusicCrawler;
    }

    public async Task<IEnumerable<SearchResult>> SearchAsync(string authorName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(authorName) || authorName.Length < MinimalSearchPatternLength)
            return Enumerable.Empty<SearchResult>();

        var result = new List<SearchResult>();

        var dbResult = await _recordRepository.SearchAuthorsByNameAsync(CapitalizeFirstLetter(authorName), cancellationToken);
        result.AddRange(dbResult.Select(SearchResultBuilder.FromDateBase));

        if (result.Count() < MinimalDbResultThreshold)
        {
            var args = new AuthorPromptArgs()
            {
                ExpectedResultLength = MaximumSearchResultLength - result.Count(),
                SearchPattern = authorName,
                AuthorsToExclude = dbResult.ToList()
            };
            var aiResponse = await _aiMusicCrawler.SearchAuthorsAsync(args, cancellationToken);
            var aiResult = PolishAiResponse(aiResponse, dbResult).Select(SearchResultBuilder.FromAi);

            result.AddRange(aiResult);
        }

        return result;
    }

    private IEnumerable<string> PolishAiResponse(IEnumerable<string> aiResponse, IEnumerable<string> dbResult)
    {
        return aiResponse.Where(x => !dbResult.Contains(x));
    }

    private string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}

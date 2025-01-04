using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Scraper.Models;
using TrackHub.Domain.Repositories;

namespace TrackHub.Scraper.Searchers.Authors;

internal class AuthorSearcher : BaseSearcher, IAuthorSearcher
{    
    private readonly IRecordRepository _recordRepository;
    private readonly IAiMusicCrawler _aiMusicCrawler;

    public AuthorSearcher(IRecordRepository recordRepository, IAiMusicCrawler aiMusicCrawler)
    {
        _recordRepository = recordRepository;
        _aiMusicCrawler = aiMusicCrawler;
    }

    public async Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string authorName, int resultSize, string[]? excludeList, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(authorName) || authorName.Length < MinimalSearchPatternLength)
            return Enumerable.Empty<ScrapperSearchResult>();

        var result = new List<ScrapperSearchResult>();

        var dbResult = await _recordRepository.SearchAuthorsByNameAsync(CapitalizeFirstLetter(authorName), resultSize, excludeList, cancellationToken);
        result.AddRange(dbResult.Select(ScrapperSearchResultBuilder.FromDateBase));

        int leftoverSize = MinimalDbResultThreshold >= resultSize ? resultSize : MinimalDbResultThreshold;
        if (result.Count() < leftoverSize)
        {
            IList<string> authorsToExclude = excludeList != null ? 
                dbResult.Union(excludeList.Select(x => x)).ToList() : dbResult.ToList();

            var args = new AuthorPromptArgs()
            {
                ExpectedResultLength = leftoverSize - result.Count(),
                SearchPattern = authorName,
                AuthorsToExclude = authorsToExclude
            };

            var aiResponse = await _aiMusicCrawler.SearchAuthorsAsync(args, cancellationToken);
            var aiResult = PolishAiResponse(aiResponse, dbResult).Select(ScrapperSearchResultBuilder.FromAi);

            result.AddRange(aiResult);
        }

        return result;
    }    
}

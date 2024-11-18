using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Searcher.Models;
using TrackHub.Domain.Repositories;

namespace TrackHub.Searcher.Searchers.Authors;

internal class AuthorSearcher : BaseSearcher, IAuthorSearcher
{    
    private readonly IRecordRepository _recordRepository;
    private readonly IAiMusicCrawler _aiMusicCrawler;

    public AuthorSearcher(IRecordRepository recordRepository, IAiMusicCrawler aiMusicCrawler)
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
}

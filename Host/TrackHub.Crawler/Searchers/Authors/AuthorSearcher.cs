using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Scraper.Models;
using TrackHub.Domain.Repositories;

namespace TrackHub.Scraper.Searchers.Authors;

internal class AuthorSearcher : IAuthorSearcher
{
    private const string CacheAuthorIdentifier = "_author";

    private readonly IRecordRepository _recordRepository;
    private readonly IAiMusicCrawler _aiMusicCrawler;
    private readonly IScraperCache _scraperCache;

    public AuthorSearcher(IRecordRepository recordRepository, IAiMusicCrawler aiMusicCrawler, IScraperCache scraperCache)
    {
        _recordRepository = recordRepository;
        _aiMusicCrawler = aiMusicCrawler;
        _scraperCache = scraperCache;   
    }

    public async Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string authorName, CancellationToken cancellationToken)
    {
        var result = new List<ScrapperSearchResult>();

        var cachedResults = _scraperCache.Get(new CacheKey(CacheAuthorIdentifier, authorName));
        if (cachedResults != null && cachedResults.Length >= Constants.MaximumSearchResultLength)
            return cachedResults.Select(ScrapperSearchResultBuilder.FromCache).ToList();       

        int leftoverLength = cachedResults == null ? Constants.MaximumSearchResultLength : Constants.MaximumSearchResultLength - cachedResults!.Length;
        var searcherResult = await SearchDbAsync(authorName, leftoverLength, cachedResults, cancellationToken);
        
        if (searcherResult.Any())
        {
            _scraperCache.Add(new CacheItem()
            {
                Key = new CacheKey(CacheAuthorIdentifier, authorName),
                Values = searcherResult.Select(x => x.Result).ToArray()
            });
        }

        return cachedResults == null ? searcherResult :
                cachedResults.Select(ScrapperSearchResultBuilder.FromCache).Union(searcherResult);
    }

    private async Task<IEnumerable<ScrapperSearchResult>> SearchDbAsync(string authorName, int resultLength, string[]? excludeList, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(authorName) || authorName.Length < Constants.MinimalSearchPatternLength)
            return Enumerable.Empty<ScrapperSearchResult>();

        var result = new List<ScrapperSearchResult>();

        var dbResult = await _recordRepository.SearchAuthorsByNameAsync(Helper.CapitalizeFirstLetter(authorName), resultLength, excludeList, cancellationToken);
        result.AddRange(dbResult.Select(ScrapperSearchResultBuilder.FromDateBase));

        int leftoverSize = Constants.MinimalDbResultThreshold >= resultLength ? resultLength : Constants.MinimalDbResultThreshold;
        if (result.Count() < leftoverSize)
        {
            IList<string> authorsToExclude = excludeList != null ? 
                dbResult.Union(excludeList).ToList() : dbResult.ToList();

           // result.AddRange(aiResult);
        }

        return result;
    }    

    private async Task<IEnumerable<ScrapperSearchResult>> SearchFromAiAsync(string authorName, int resultSize, string[]? authorsToExclude, CancellationToken cancellationToken)
    {
        var args = new AuthorPromptArgs()
        {
            ExpectedResultLength = resultSize,
            SearchPattern = authorName,
            AuthorsToExclude = authorsToExclude
        };

        var aiResponse = await _aiMusicCrawler.SearchAuthorsAsync(args, cancellationToken);
        return aiResponse.Select(ScrapperSearchResultBuilder.FromAi);
    }
}

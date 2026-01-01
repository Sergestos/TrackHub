using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Service.Scraper.Models;
using TrackHub.Domain.Repositories;

namespace TrackHub.Service.Scraper.Searchers.Authors;

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

    public async Task<IEnumerable<ScraperSearchResult>> SearchAsync(string authorName, CancellationToken cancellationToken)
    {
        var result = new List<ScraperSearchResult>();

        var cachedResults = _scraperCache.Get(new CacheKey(CacheAuthorIdentifier, authorName));
        if (cachedResults != null && cachedResults.Length >= Constants.MaximumSearchResultLength)
            return cachedResults.Select(ScraperSearchResultBuilder.FromCache).ToList();       

        int leftoverLength = cachedResults == null ? Constants.MaximumSearchResultLength : Constants.MaximumSearchResultLength - cachedResults!.Length;
        var searcherResult = await SearchDbAndAiAsync(authorName, leftoverLength, cachedResults, cancellationToken);
        
        if (searcherResult.Any())
        {
            _scraperCache.Add(new CacheItem()
            {
                Key = new CacheKey(CacheAuthorIdentifier, authorName),
                Values = searcherResult.Select(x => x.Result).ToArray()
            });
        }

        return cachedResults == null ? searcherResult :
                cachedResults.Select(ScraperSearchResultBuilder.FromCache).Union(searcherResult);
    }

    private async Task<IEnumerable<ScraperSearchResult>> SearchDbAndAiAsync(string authorName, int resultLength, string[]? excludeList, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(authorName) || authorName.Length < Constants.MinimalSearchPatternLength)
            return Enumerable.Empty<ScraperSearchResult>();

        var result = new List<ScraperSearchResult>();

        var dbResult = await _recordRepository.SearchAuthorsByNameAsync(Helper.CapitalizeFirstLetter(authorName), resultLength, excludeList, cancellationToken);
        result.AddRange(dbResult.Select(ScraperSearchResultBuilder.FromDateBase));

        int leftoverSize = Constants.MinimalDbResultThreshold >= resultLength ? resultLength : Constants.MinimalDbResultThreshold;
        if (result.Count() < leftoverSize)
        {
            IList<string> authorsToExclude = excludeList != null ? 
                dbResult.Union(excludeList).ToList() : dbResult.ToList();

            var args = new AuthorPromptArgs()
            {
                ExpectedResultLength = Constants.MaximumSearchResultLength - result.Count(),
                SearchPattern = authorName,
            };
            var aiResponse = await _aiMusicCrawler.SearchAuthorsAsync(args, cancellationToken);

            if (aiResponse != null)
            {
                var aiResult = aiResponse.Where(x => !dbResult.Contains(x)).Select(ScraperSearchResultBuilder.FromAi);
                result.AddRange(aiResult);
            }
        }

        return result;
    }    
}

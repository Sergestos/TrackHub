using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Domain.Repositories;
using TrackHub.Service.Scraper.Models;

namespace TrackHub.Service.Scraper.Searchers.Song;

internal class SongSearcher : ISongSearcher
{
    private const string CacheSongIdentifier = "_song";

    private readonly IRecordRepository _recordRepository;
    private readonly IAiMusicCrawler _aiMusicCrawler;
    private readonly IScraperCache _scraperCache;

    public SongSearcher(IRecordRepository recordRepository, IAiMusicCrawler aiMusicCrawler, IScraperCache scraperCache)
    {
        _recordRepository = recordRepository;
        _aiMusicCrawler = aiMusicCrawler;
        _scraperCache = scraperCache;
    }

    public async Task<IEnumerable<ScraperSearchResult>> SearchAsync(string songName, int resultSize, CancellationToken cancellationToken)
    { 
        var result = new List<ScraperSearchResult>();

        var cachedResults = _scraperCache.Get(new CacheKey(CacheSongIdentifier, songName));
        if (cachedResults != null && cachedResults.Length >= Constants.MaximumSearchResultLength)
            return cachedResults.Select(ScraperSearchResultBuilder.FromCache).ToList();

        int leftoverLength = cachedResults == null ? Constants.MaximumSearchResultLength : Constants.MaximumSearchResultLength - cachedResults!.Length;
        var searcherResult = await SearchDbAndAiAsync(songName, leftoverLength, cachedResults, cancellationToken);

        if (searcherResult.Any())
        {          
            _scraperCache.Add(new CacheItem()
            {
                Key = new CacheKey(CacheSongIdentifier, songName),
                Values = searcherResult.Select(x => x.Result).ToArray()
            });
        }

        return cachedResults == null ? searcherResult :
                cachedResults.Select(ScraperSearchResultBuilder.FromCache).Union(searcherResult);
    }

    private async Task<IEnumerable<ScraperSearchResult>> SearchDbAndAiAsync(string pattern, int resultSize, string[]? excludeList, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(pattern) || pattern.Length < Constants.MinimalSearchPatternLength)
            return Enumerable.Empty<ScraperSearchResult>();

        var result = new List<ScraperSearchResult>();

        var dbResult = await _recordRepository.SearchSongsByNameAsync(Helper.CapitalizeFirstLetter(pattern), resultSize, excludeList, cancellationToken);
        result.AddRange(dbResult.Select(ScraperSearchResultBuilder.FromDateBase));

        int leftoverSize = Constants.MinimalDbResultThreshold >= resultSize ? resultSize : Constants.MinimalDbResultThreshold;
        if (result.Count() < Constants.MinimalDbResultThreshold)
        {
            IList<string> songsToExclude = excludeList != null ?
                dbResult.Union(excludeList).ToList() : dbResult.ToList();

            var args = new SongPromptArgs()
            {
                ExpectedResultLength = Constants.MaximumSearchResultLength - result.Count(),
                SearchPattern = pattern,
                AlbumsToExclude = null,
                AlbumsToInclude = null,
                AuthorName = null
            };
            var aiResponse = await _aiMusicCrawler.SearchSongsAsync(args, cancellationToken);

            if (aiResponse != null)
            {
                var aiResult = aiResponse.Where(x => !dbResult.Contains(x)).Select(ScraperSearchResultBuilder.FromAi);
                result.AddRange(aiResult);
            }            
        }

        return result;
    }
}

using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Domain.Repositories;
using TrackHub.Scraper.Models;

namespace TrackHub.Scraper.Searchers.Song;

internal class SongSearcher : BaseSearcher, ISongSearcher
{
    private readonly IRecordRepository _recordRepository;
    private readonly IAiMusicCrawler _aiMusicCrawler;

    public SongSearcher(IRecordRepository recordRepository, IAiMusicCrawler aiMusicCrawler)
    {
        _recordRepository = recordRepository;
        _aiMusicCrawler = aiMusicCrawler;
    }

    public async Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string pattern, string authorName, int resultSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ScrapperSearchResult>> SearchAsync(string pattern, int resultSize, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(pattern) || pattern.Length < MinimalSearchPatternLength)
            return Enumerable.Empty<ScrapperSearchResult>();

        var result = new List<ScrapperSearchResult>();

        var dbResult = await _recordRepository.SearchSongsByNameAsync(CapitalizeFirstLetter(pattern), resultSize, null, cancellationToken);
        result.AddRange(dbResult.Select(ScrapperSearchResultBuilder.FromDateBase));

        int leftoverSize = MinimalDbResultThreshold >= resultSize ? resultSize : MinimalDbResultThreshold;
        if (result.Count() < MinimalDbResultThreshold)
        {
            var args = new SongPromptArgs()
            {
                ExpectedResultLength = MaximumSearchResultLength - result.Count(),
                SearchPattern = pattern,
                AlbumsToExclude = null,
                AlbumsToInclude = null
            };
            var aiResponse = await _aiMusicCrawler.SearchSongsAsync(args, cancellationToken);
            var aiResult = PolishAiResponse(aiResponse, dbResult).Select(ScrapperSearchResultBuilder.FromAi);

            result.AddRange(aiResult);
        }

        return result;
    }
}

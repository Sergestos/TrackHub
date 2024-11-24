using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Domain.Repositories;
using TrackHub.Searcher.Models;

namespace TrackHub.Searcher.Searchers.Song;

internal class SongSearcher : BaseSearcher, ISongSearcher
{
    private readonly IRecordRepository _recordRepository;
    private readonly IAiMusicCrawler _aiMusicCrawler;

    public SongSearcher(IRecordRepository recordRepository, IAiMusicCrawler aiMusicCrawler)
    {
        _recordRepository = recordRepository;
        _aiMusicCrawler = aiMusicCrawler;
    }

    public async Task<IEnumerable<SearchResult>> SearchAsync(string pattern, string authorName, int resultSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SearchResult>> SearchAsync(string pattern, int resultSize, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(pattern) || pattern.Length < MinimalSearchPatternLength)
            return Enumerable.Empty<SearchResult>();

        var result = new List<SearchResult>();

        var dbResult = await _recordRepository.SearchSongsByNameAsync(CapitalizeFirstLetter(pattern), resultSize, cancellationToken);
        result.AddRange(dbResult.Select(SearchResultBuilder.FromDateBase));

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
            var aiResult = PolishAiResponse(aiResponse, dbResult).Select(SearchResultBuilder.FromAi);

            result.AddRange(aiResult);
        }

        return result;
    }
}

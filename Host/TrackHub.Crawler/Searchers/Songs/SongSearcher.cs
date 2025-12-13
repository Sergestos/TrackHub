using TrackHub.AiCrawler;
using TrackHub.AiCrawler.PromptModels;
using TrackHub.Domain.Repositories;
using TrackHub.Service.Scrapper.Models;

namespace TrackHub.Service.Scrapper.Searchers.Song;

internal class SongSearcher : ISongSearcher
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
        if (string.IsNullOrWhiteSpace(pattern) || pattern.Length < Constants.MinimalSearchPatternLength)
            return Enumerable.Empty<ScrapperSearchResult>();

        var result = new List<ScrapperSearchResult>();

        var dbResult = await _recordRepository.SearchSongsByNameAsync(Helper.CapitalizeFirstLetter(pattern), resultSize, null, cancellationToken);
        result.AddRange(dbResult.Select(ScrapperSearchResultBuilder.FromDateBase));

        int leftoverSize = Constants.MinimalDbResultThreshold >= resultSize ? resultSize : Constants.MinimalDbResultThreshold;
        if (result.Count() < Constants.MinimalDbResultThreshold)
        {
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
                var aiResult = aiResponse.Where(x => !dbResult.Contains(x)).Select(ScrapperSearchResultBuilder.FromAi);
                result.AddRange(aiResult);
            }            
        }

        return result;
    }
}

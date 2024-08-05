using TrackHub.AiCrawler.PromtModels;

namespace TrackHub.AiCrawler;

public interface IMusicCrawler
{
    Task<IEnumerable<string>> SearchSongsAsync(SongPromptArgs args, CancellationToken token);

    Task<IEnumerable<string>> SearchAuthorsAsync(GeneralPromptArgs args, CancellationToken token);
}

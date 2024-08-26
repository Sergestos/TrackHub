using TrackHub.AiCrawler.PromptModels;

namespace TrackHub.AiCrawler;

public interface IAiMusicCrawler
{
    Task<IEnumerable<string>> SearchSongsAsync(SongPromptArgs args, CancellationToken token);

    Task<IEnumerable<string>> SearchAuthorsAsync(GeneralPromptArgs args, CancellationToken token);
}

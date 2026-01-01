namespace TrackHub.AiCrawler.PromptModels;

public class SongPromptArgs : GeneralPromptArgs
{    
    public IEnumerable<string>? AlbumsToInclude { get; set; }

    public IEnumerable<string>? AlbumsToExclude { get; set; }

    public string? AuthorName { get; set; }
}

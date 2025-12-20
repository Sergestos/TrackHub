namespace TrackHub.AiCrawler.PromptModels;

public class AuthorPromptArgs : GeneralPromptArgs
{
    public IList<string>? AuthorsToExclude { get; set; }
}

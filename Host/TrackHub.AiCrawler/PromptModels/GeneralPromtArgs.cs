namespace TrackHub.AiCrawler.PromptModels;

public class GeneralPromptArgs
{
    public int ExpectedResultLength { get; set; } = 5;

    public string? SearchPattern { get; set; }
}

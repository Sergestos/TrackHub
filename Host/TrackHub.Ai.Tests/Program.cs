using TrackHub.AiCrawler;
using TrackHub.AiCrawler.OpenAI;
using TrackHub.AiCrawler.PromptModels;

internal class Program
{
    private static void Main(string[] args)
    { 
        var songPromptArgsargs = new SongPromptArgs()
        {
            SearchPattern = "Live",
            ExpectedResultLength = 5,
            AlbumsToExclude = null,
            AlbumsToInclude = null
        };

        IAiMusicCrawler crawler = new OpenAIMusicCrawler(null);
        var result = crawler.SearchSongsAsync(songPromptArgsargs, CancellationToken.None).Result;

        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
            
        Console.ReadKey();
    }
}
 
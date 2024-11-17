using Microsoft.Extensions.Configuration;
using TrackHub.AiCrawler.PromptModels;

namespace TrackHub.AiCrawler.OpenAI;

public class OpenAIMusicCrawler : AbstractConversation, IAiMusicCrawler
{
    public OpenAIMusicCrawler(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<string>> SearchSongsAsync(SongPromptArgs args, CancellationToken token)
    {        
        var conversation = GetConversation(args);

        conversation.AppendUserInput(Prompts.SearchForSongs);        

        if (args.AlbumsToInclude != null && args.AlbumsToInclude.Any())
            conversation.AppendUserInput(Prompts.IncludeSongsFromAlbums + args.AlbumsToInclude.Select(x => x + ";"));

        if (args.AlbumsToExclude != null && args.AlbumsToExclude.Any())
            conversation.AppendUserInput(Prompts.ExcludeSongsFromAlbums + args.AlbumsToExclude.Select(x => x + ";"));

        return await GetAiResponse(conversation);
    }

    public async Task<IEnumerable<string>> SearchAuthorsAsync(AuthorPromptArgs args, CancellationToken token)
    {
        var conversation = GetConversation(args);

        conversation.AppendUserInput(Prompts.SearchForAuthors);

        if (args.AuthorsToExclude != null && args.AuthorsToExclude.Any())
            conversation.AppendUserInput(Prompts.ExcludeAuthors + args.AuthorsToExclude.Select(x => x + ","));

        return await GetAiResponse(conversation);
    }
}

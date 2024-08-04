using TrackHub.AI.PromtModels;

namespace TrackHub.AI.OpenAI;

internal class OpenAIMusicCrawler : AbstractConversation, IMusicCrawler
{
    public async Task<IEnumerable<string>> SearchSongsAsync(SongPromptArgs args, CancellationToken token)
    {        
        var conversation = GetConversation(args);

        conversation.AppendUserInput("Search for existing songs. Return names of these songs.");

        if (args.AlbumsToInclude != null && args.AlbumsToInclude.Any())
            conversation.AppendUserInput(Prompts.IncludeSongsFromAlbums + args.AlbumsToInclude.Select(x => x + ";"));

        if (args.AlbumsToExclude != null && args.AlbumsToExclude.Any())
            conversation.AppendUserInput(Prompts.ExcludeSongsFromAlbums + args.AlbumsToExclude.Select(x => x + ";"));

        return await GetAiResponse(conversation);
    }

    public async Task<IEnumerable<string>> SearchAuthorsAsync(GeneralPromptArgs args, CancellationToken token)
    {
        var conversation = GetConversation(args);

        conversation.AppendUserInput("Search for existing authors of Music bands. Return names of these bands.");

        return await GetAiResponse(conversation);
    }
}

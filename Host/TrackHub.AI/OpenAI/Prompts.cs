namespace TrackHub.AiCrawler.OpenAI;

internal static class Prompts
{
    internal static string ConversationTopic = @"
        This conversation is about rock, metal and blues music.
        Exclude other genres, like rap or pop.
        User will request certain information.";

    internal static string ResponseFormat = @"
        This information should be responded as a list.
        Any additional text should be droped.
        Result should be a string of values separated with a comma.";

    internal static string PopularAssets = @"
        Prioritize most popular items in the final processing.";

    internal static string IncludeSongsFromAlbums = @"
        Include songs from the next albums: ";

    internal static string ExcludeSongsFromAlbums = @"
        Exclude songs from the next albums: ";

    internal static string SearchForSongs = @"
        Search for existing songs. Return names of these songs and their authors.
        Names(pure name of a song, author or band name is not considering as a part of song name)
        of these songs must begin the next pattern. This patten might be a separate word or just a part of a word.
        The pattern was provided previously.";

    internal static string SearchForAuthors = @"
        Search for existing authors of Music bands. Return names of these bands.";
}

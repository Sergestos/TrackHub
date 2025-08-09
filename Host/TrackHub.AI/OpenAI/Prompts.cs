namespace TrackHub.AiCrawler.OpenAI;

internal static class Prompts
{
    internal static string ConversationTopic = @"
        This conversation is about rock, metal and blues music.
        Exclude other genres, like rap or pop.
        User will request certain information.";

    internal static string ResponseFormat = @"
        This information should be responded as a list.
        Any additional text should be dropped.
        Result should be a string of values separated with a comma.
        Don't send any other information in any other format, respond only as asked";

    internal static string PopularAssets = @"
        Prioritize most popular in the final processing.
        If you asked to search songs, prioritize most popular songs.
        If you asked to search authors or bands, prioritize most popular authors or bands.";

    internal static string ExpectedResultLength = @"
        Limit result in a list. Limit is ";

    internal static string IncludeSongsFromAlbums = @"
        Include songs from the next albums: ";

    internal static string ExcludeSongsFromAlbums = @"
        Exclude songs from the next albums: ";

    internal static string SearchForSongs = @"
        Search for existing songs. Return names of these songs name only, drop authors names.
        Names(pure name of a song, author or band name is not considering as a part of song name)
        of these songs must begin the next pattern. This patten might be a separate word or just a part of a word.
        Prioritize most popular songs, but they anyway must start with pattern provided previously.
        The pattern was provided previously.";

    internal static string SearchForAuthors = @"
        Search for existing authors of Music bands. Return names of these bands or authors.
        Prioritize most popular authors or bands, but they anyway must start with pattern provided previously.
        Any authors or bands those are not start with provided pattern must not be included into result.";

    internal static string ExcludeAuthors = @"
        Exclude some authors or bands. They must not be included into final result.
        Just ignore them. There is a list to ignore: ";

    internal static string NoResponseFormat = @"
        If you ca'nt assist or find any results, just return empty string. Don't return anything else.";
}

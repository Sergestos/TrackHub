namespace TrackHub.AI.OpenAI;

internal class Prompts
{
    internal static string ConversationTopic = @"
        This conversation is about music.
        User will request certain information.";

    internal static string ResponseFormat = @"
        This information should be responded as a list.
        Any additional text should be droped.
        Result should be a string of values separated with a comma.";

    internal static string IncludeSongsFromAlbums = @"
        Include songs from the next albums: 
    ";

    internal static string ExcludeSongsFromAlbums = @"
        Exclude songs from the next albums: 
    ";
}

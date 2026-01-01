using System.Text.RegularExpressions;

public static class UserSongIds
{    
    public static string Transform(string author, string songName)
    {
        return $"{Normalize(author)}|{Normalize(songName)}";
    }

    private static string Normalize(string value)
    {
        value = value.ToLowerInvariant();
        value = Regex.Replace(value, @"\s+", "_");      
        value = Regex.Replace(value, @"[^a-z0-9_]", ""); 
        return value;
    }
}

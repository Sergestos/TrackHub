namespace TrackHub.Seacher.Models;

internal class CacheItem
{
    public required CacheKey Key { get; set; }

    public required string[] Values { get; set; }
}

internal class CacheKey
{
    public CacheKey(string setIdentifier, string pattern)
    {
        SetIdentifier = setIdentifier;
        Pattern = pattern;
    }

    public string SetIdentifier { get; set; }

    public string Pattern { get; set; }
}

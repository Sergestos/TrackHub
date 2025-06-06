namespace TrackHub.Scraper;

internal static class Helper
{ 
    internal static string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}

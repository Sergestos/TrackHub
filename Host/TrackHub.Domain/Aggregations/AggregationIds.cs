
using System.Text.RegularExpressions;

namespace TrackHub.Domain.Aggregations;

public static class AggregationIds
{
    public static string Monthly(string userId, int year, int month)
        => $"agg|monthly|{userId}|{year:D4}-{month:D2}";

    public static string Monthly(string userId, DateTime date)
        => Monthly(userId, date.Year, date.Month);

    public static string Song(
    string userId,
    string band,
    string song)
    {
        return $"agg|song|{Normalize(userId)}|{Slug(band)}|{Slug(song)}";
    }

    private static string Slug(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "unknown";

        value = value.Trim().ToLowerInvariant();

        value = Regex.Replace(value, @"\s+", "_");

        value = Regex.Replace(value, @"[^a-z0-9_]", "");

        return value;
    }

    private static string Normalize(string value)
        => value.Trim().ToLowerInvariant();
}
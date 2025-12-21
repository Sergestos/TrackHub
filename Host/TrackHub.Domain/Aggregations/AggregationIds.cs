
namespace TrackHub.Domain.Aggregations;

public static class AggregationIds
{
    public static string Monthly(string userId, int year, int month)
        => $"agg|monthly|{userId}|{year:D4}-{month:D2}";

    public static string Monthly(string userId, DateTime date)
        => Monthly(userId, date.Year, date.Month);
}
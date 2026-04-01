
namespace TrackHub.Domain.Consistency;

public static class DaysTrendAggregationId
{
    public static string GetUserRecentTrendId(string usedId)
    {
        return "recent_tredns_" + usedId;
    }
}

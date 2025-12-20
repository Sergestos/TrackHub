namespace TrackHub.Messaging.Aggregations;

public class AggregationEventMessage
{
    public required string UserId { get; set; }

    public required DateTime EventDate { get; set; }

    public required AggregationRecord NewRecord { get; set; }

    public required AggregationRecord OldRecord { get; set; }
}

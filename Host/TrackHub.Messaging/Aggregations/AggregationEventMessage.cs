namespace TrackHub.Messaging.Aggregations;

public class AggregationEventMessage
{
    public required string UserId { get; set; }

    public required DateTime EventDate { get; set; }

    public required DateTime PlayDate { get; set; }

    public required AggregationRecord[] NewRecords { get; set; }

    public AggregationRecord[]? OldRecords { get; set; }
}

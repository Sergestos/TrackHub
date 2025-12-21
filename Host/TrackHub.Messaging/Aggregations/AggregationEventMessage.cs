namespace TrackHub.Messaging.Aggregations;

public class AggregationEventMessage
{
    public required string UserId { get; set; }

    public required DateTime EventDate { get; set; }

    public required DateTime PlayDate { get; set; }

    public required AggregatedRecordState[] AggregatedRecordStates { get; set; }
}

public class AggregatedRecordState
{
    public required AggregationRecord NewRecord { get; set; }

    public AggregationRecord? OldRecord { get; set; }
}
namespace TrackHub.Messaging.Aggregations;

public class AggregationRecord
{
    public required string RecordId { get; set; }

    public required int PlayDuration { get; set; }

    public required RecordType RecordType { get; set; }

    public required PlayType PlayType { get; set; }
}

public enum RecordType
{
    Warmup,
    Song,
    Improvisation,
    Exercise,
    Composing
}

public enum PlayType
{
    Rhythm,
    Solo,
    Both
}

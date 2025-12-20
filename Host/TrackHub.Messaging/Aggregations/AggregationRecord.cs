using TrackHub.Domain.Enums;

namespace TrackHub.Messaging.Aggregations;

public class AggregationRecord
{
    public required int PlayDuration { get; set; }

    public string? Author { get; set; }

    public string? Name { get; set; }

    public required RecordType RecordType { get; set; }

    public required PlayType PlayType { get; set; }
}

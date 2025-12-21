using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Aggregators;

public interface ISongAggregator
{
    Task AggregateSong(AggregationEventMessage message, CancellationToken cancellationToken);
}

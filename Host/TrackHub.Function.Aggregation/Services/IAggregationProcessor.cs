using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Services;

public interface IAggregationProcessor
{
    Task Process(AggregationEventMessage message, CancellationToken cancellationToken);
}

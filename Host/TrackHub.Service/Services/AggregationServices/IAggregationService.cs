using TrackHub.Messaging.Aggregations;

namespace TrackHub.Service.Aggregation.Services;

public interface IAggregationService
{
    Task SendAggregationAsync(AggregationEventMessage payload, CancellationToken cancellationToken = default);
}

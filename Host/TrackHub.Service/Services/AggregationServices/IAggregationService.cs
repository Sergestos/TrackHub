using TrackHub.Messaging.Aggregations;

namespace TrackHub.Service.Aggregation.Services;

public interface IAggregationService
{
    void SendAggregation(AggregationEventMessage payload);
}

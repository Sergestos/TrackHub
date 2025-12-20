using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Services;

internal class AggregationProcessor : IAggregationProcessor
{
    public Task Process(AggregationEventMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

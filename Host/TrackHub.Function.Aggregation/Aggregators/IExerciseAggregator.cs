using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Aggregators;

public interface IExerciseAggregator
{
    Task AggregateExercise(AggregationEventMessage message, CancellationToken cancellationToken);
}

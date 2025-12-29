using TrackHub.Domain.Aggregations;

namespace TrackHub.Service.Services.AggregationServices;

public interface IAggregationReadService
{
    Task<ExerciseAggregation?> GetExerciseAggregationByDateAsync(string userId, DateTime date, CancellationToken cancellation);

    Task<IEnumerable<ExerciseAggregation>?> GetExerciseAggregationsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate, CancellationToken cancellation);
}

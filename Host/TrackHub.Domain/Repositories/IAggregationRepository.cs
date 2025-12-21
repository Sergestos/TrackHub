using TrackHub.Domain.Aggregations;

namespace TrackHub.Domain.Repositories;

public interface IAggregationRepository
{
    Task<ExerciseAggregation> UpsertAggregation(string userId, ExerciseAggregation aggregation, CancellationToken cancellationToken);

    Task<ExerciseAggregation?> GetById(string id, string userId, CancellationToken cancellationToken);
}

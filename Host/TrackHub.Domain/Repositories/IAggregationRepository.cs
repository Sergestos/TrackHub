using TrackHub.Domain.Aggregations;

namespace TrackHub.Domain.Repositories;

public interface IAggregationRepository
{
    Task<ExerciseAggregation> UpsertAggregation(ExerciseAggregation aggregation, CancellationToken cancellationToken);

    Task<ExerciseAggregation> GetById(string id, CancellationToken cancellationToken);
}

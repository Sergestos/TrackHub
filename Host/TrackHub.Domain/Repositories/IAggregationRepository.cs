using TrackHub.Domain.Aggregations;

namespace TrackHub.Domain.Repositories;

public interface IAggregationRepository
{
    Task<ExerciseAggregation?> GetExerciseAggregationById(string aggregationId, string userId, CancellationToken cancellationToken);

    Task<IEnumerable<ExerciseAggregation>?> GetExerciseAggregationsByIds(string[] aggregationIds, string userId, CancellationToken cancellationToken);

    Task<SongAggregation?> GetSongAggregationById(string aggregationId, string userId, CancellationToken cancellationToken);

    Task<ExerciseAggregation> UpsertExerciseAggregation(string userId, ExerciseAggregation aggregation, CancellationToken cancellationToken);

    Task<SongAggregation> UpsertSongAggregation(string userId, SongAggregation aggregation, CancellationToken cancellationToken);

    Task<IEnumerable<SongAggregation>> UpsertSongAggregations(string userId, SongAggregation[] aggregations, CancellationToken cancellationToken);
}

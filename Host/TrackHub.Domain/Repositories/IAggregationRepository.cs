using TrackHub.Domain.Aggregations;

namespace TrackHub.Domain.Repositories;

public interface IAggregationRepository
{
    Task<ExerciseAggregation?> GetExerciseAggregationByIdAsync(string aggregationId, string userId, CancellationToken cancellationToken);

    Task<IEnumerable<ExerciseAggregation>?> GetExerciseAggregationListByIdsAsync(string[] aggregationIds, string userId, CancellationToken cancellationToken);

    Task<SongAggregation?> GetSongAggregationByIdAsync(string aggregationId, string userId, CancellationToken cancellationToken);

    Task<IEnumerable<SongAggregation>> GetSongAggregationsByUserIdAsync(string userId, CancellationToken cancellationToken);

    Task<IEnumerable<SongAggregation>> GetSongAggregationListByIdsAsync(string userId, string[] songAggregationIds, CancellationToken cancellationToken);

    Task<IEnumerable<SongAggregation>> GetSongAggregationListByDateAsync(string userId, DateOnly date, CancellationToken cancellationToken);    

    Task<DaysTrendAggregation> GetDaysTrendAggregation(string userId, CancellationToken cancellationToken);

    Task<ExerciseAggregation> UpsertExerciseAggregationAsync(string userId, ExerciseAggregation aggregation, CancellationToken cancellationToken);

    Task<SongAggregation> UpsertSongAggregationAsync(string userId, SongAggregation aggregation, CancellationToken cancellationToken);

    Task<IEnumerable<SongAggregation>> UpsertSongAggregationsAsync(string userId, SongAggregation[] aggregations, CancellationToken cancellationToken);

    Task<DaysTrendAggregation> UpsertDaysTrendAggregation(string userId, DaysTrendAggregation aggregation, CancellationToken cancellationToken);
}

using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Consistency;
using TrackHub.Domain.Repositories;

namespace TrackHub.Service.Services.AggregationServices;

internal class AggregationReadService : IAggregationReadService
{
    private readonly IAggregationRepository _aggregationRepository;
    private readonly IUserRepository _userRepository;
    public AggregationReadService(IAggregationRepository aggregationRepository, IUserRepository userRepository)
    {
        _aggregationRepository = aggregationRepository;
        _userRepository = userRepository;
    }

    public async Task<ExerciseAggregation?> GetExerciseAggregationByDateAsync(string userId, DateTime date, CancellationToken cancellationToken)
    {
        var aggregationId = AggregationIds.Monthly(userId, date);

        return await _aggregationRepository.GetExerciseAggregationById(aggregationId, userId, cancellationToken);
    }

    public async Task<IEnumerable<ExerciseAggregation>?> GetExerciseAggregationsByDateRangeAsync(
        string userId, 
        DateTime startDate, 
        DateTime endDate, 
        CancellationToken cancellationToken)
    {
        string[] aggregationIds = GetMonthYearRange(startDate, endDate)
            .Select(date => AggregationIds.Monthly(userId, date))
            .ToArray();

        return await _aggregationRepository.GetExerciseAggregationListByIds(aggregationIds, userId, cancellationToken);
    }

    private static IReadOnlyList<DateTime> GetMonthYearRange(
        DateTime startDate,
        DateTime endDate)
    {
        var start = new DateTime(startDate.Year, startDate.Month, 1);
        var end = new DateTime(endDate.Year, endDate.Month, 1);

        var result = new List<DateTime>();

        for (var dt = start; dt <= end; dt = dt.AddMonths(1))
        {
            result.Add(dt);
        }

        return result;
    }

    public async Task<IEnumerable<SongAggregation>> GetSongAggregationsAsync(string userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserById(userId);
        if (user!.OrderedByDurationPlayedSongs is null)
            return Enumerable.Empty<SongAggregation>();
        
        string[] songIds = user!.OrderedByDurationPlayedSongs
            .Skip((page - 1) * pageSize)
            .Take(pageSize)            
            .ToArray();

        return await _aggregationRepository.GetSongAggregationListByIds(userId, songIds, cancellationToken);
    }
}

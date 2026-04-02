using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Consistency;
using TrackHub.Domain.Repositories;

namespace TrackHub.Service.Services.AggregationServices;

internal class AggregationReadService : IAggregationReadService
{
    private readonly IAggregationRepository _aggregationRepository;
    private readonly IAggregationService _aggregationService;
    private readonly IUserRepository _userRepository;
    public AggregationReadService(IAggregationRepository aggregationRepository, IAggregationService aggregationService, IUserRepository userRepository)
    {
        _aggregationRepository = aggregationRepository;
        _aggregationService = aggregationService;
        _userRepository = userRepository;
    }

    public async Task<ExerciseAggregation?> GetExerciseAggregationByDateAsync(string userId, DateTime date, CancellationToken cancellationToken)
    {
        var aggregationId = AggregationIds.Monthly(userId, date);

        return await _aggregationRepository.GetExerciseAggregationByIdAsync(aggregationId, userId, cancellationToken);
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

        return await _aggregationRepository.GetExerciseAggregationListByIdsAsync(aggregationIds, userId, cancellationToken);
    }

    public async Task<DaysTrendAggregation> GetDaysTrendAggregationsAsync(string userId, CancellationToken cancellationToken)
    {
        var result = await _aggregationRepository.GetDaysTrendAggregation(userId, cancellationToken);
        if (result == null || result.BuildDate.Date != DateTime.Now.Date)        
            return await _aggregationService.BuildDaysTrendAsync(userId, cancellationToken);        

        return result;
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

    public async Task<IEnumerable<SongAggregation>> GetSongAggregationsAsync(string userId, int page, int pageSize, DateOnly? date, CancellationToken cancellationToken)
    {
        if (date == null)
        {
            return await GetSongAggregationByTopOrderAsync(userId, page, pageSize, cancellationToken);
        }
        else
        {
            return await GetSongAggregationByDateAsync(userId, date.Value, cancellationToken);
        }
    }

    private async Task<IEnumerable<SongAggregation>> GetSongAggregationByDateAsync(string userId, DateOnly date, CancellationToken cancellationToken)
    {
        return await _aggregationRepository.GetSongAggregationListByDateAsync(userId, date, cancellationToken);
    }

    private async Task<IEnumerable<SongAggregation>> GetSongAggregationByTopOrderAsync(string userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserById(userId);
        if (user!.OrderedByDurationPlayedSongs is null)
            return Enumerable.Empty<SongAggregation>();

        string[] songIds = user!.OrderedByDurationPlayedSongs
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArray();

        return await _aggregationRepository.GetSongAggregationListByIdsAsync(userId, songIds, cancellationToken);
    }
}

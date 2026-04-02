using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Consistency;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;
using TrackHub.Domain.Repositories;

namespace TrackHub.Service.Services.AggregationServices;

internal class AggregationService : IAggregationService
{
    private const int LAST_COUNT = 30;

    private readonly IAggregationRepository _aggregationRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public AggregationService(IAggregationRepository aggregationRepository, IExerciseRepository exerciseRepository)
    {
        _aggregationRepository = aggregationRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<DaysTrendAggregation> BuildDaysTrendAsync(string userId, CancellationToken cancellationToken)
    {
        var exercisesList = await _exerciseRepository.GetExerciseListByLastDaysAsync(LAST_COUNT, userId, cancellationToken);

        DaysTrendAggregation aggregation = new DaysTrendAggregation()
        {
            AggregationId = DaysTrendAggregationId.GetUserRecentTrendId(userId),
            BuildDate = DateTime.Now,      
            UserId = userId
        };

        foreach (var exercise in exercisesList)
        {
            aggregation.DaysTrendBarList!.Add(BuildDayTrendBar(exercise));
        }

        return await _aggregationRepository.UpsertDaysTrendAggregation(userId, aggregation, cancellationToken);
    }

    public async Task UpsertDayTrendBarAsync(string userId, DateTime dateTime, CancellationToken cancellationToken)
    {
        var aggregation = await _aggregationRepository.GetDaysTrendAggregation(userId, cancellationToken);
        if (aggregation == null)
        {
            aggregation = await BuildDaysTrendAsync(userId, cancellationToken);
        }        
        else if (aggregation.BuildDate.Date != DateTime.Now.Date)
        {
            var exercise = _exerciseRepository.GetExerciseByDate(DateOnly.FromDateTime(dateTime), userId, cancellationToken);
            if (exercise != null)
            {
                var newBar = BuildDayTrendBar(exercise);
                var affectedBar = aggregation.DaysTrendBarList!.FirstOrDefault(x => x.PlayDate.Date == newBar.PlayDate.Date);
                affectedBar = newBar;

                await _aggregationRepository.UpsertDaysTrendAggregation(userId, aggregation, cancellationToken);
            }
            else
            {
                var affectedBar = aggregation.DaysTrendBarList!.FirstOrDefault(x => x.PlayDate.Date == dateTime.Date);
                affectedBar = null;

                await _aggregationRepository.UpsertDaysTrendAggregation(userId, aggregation, cancellationToken);
            }
        }
    }    

    private DayTrendBar BuildDayTrendBar(Exercise exercise)
    {
        return new DayTrendBar()
        {
            PlayDate = exercise.PlayDate,
            TotalPlayedRhythmDuration = exercise.Records.Where(x => x.PlayType == PlayType.Rhythm).Sum(x => x.PlayDuration),
            TotalPlayedSoloDuration = exercise.Records.Where(x => x.PlayType == PlayType.Solo).Sum(x => x.PlayDuration),
            TotalPlayedBothDuration = exercise.Records.Where(x => x.PlayType == PlayType.Both).Sum(x => x.PlayDuration),
            TotalWarmupDuration = exercise.Records.Where(x => x.RecordType == RecordType.Warmup).Sum(x => x.PlayDuration),
            TotalSongDuration = exercise.Records.Where(x => x.RecordType == RecordType.Song).Sum(x => x.PlayDuration),
            TotalImprovisationDuration = exercise.Records.Where(x => x.RecordType == RecordType.Improvisation).Sum(x => x.PlayDuration),
            TotalPracticalExerciseDuration = exercise.Records.Where(x => x.RecordType == RecordType.Exercise).Sum(x => x.PlayDuration),
            TotalComposingDuration = exercise.Records.Where(x => x.RecordType == RecordType.Composing).Sum(x => x.PlayDuration),
        };
    }
}

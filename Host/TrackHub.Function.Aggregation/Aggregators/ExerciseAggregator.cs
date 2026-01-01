using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Consistency;
using TrackHub.Domain.Enums;
using TrackHub.Domain.Repositories;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Aggregators;

internal class ExerciseAggregator : IExerciseAggregator
{
    private const string AggregationTypeName = "exercise_aggregation";

    private readonly IAggregationRepository _aggregationRepository;

    public ExerciseAggregator(IAggregationRepository aggregationRepository)
    {
        _aggregationRepository = aggregationRepository;
    }

    public async Task AggregateExercise(AggregationEventMessage message, CancellationToken cancellationToken)
    {
        var aggregationIds = AggregationIds.Monthly(message.UserId, message.PlayDate.Date);
        ExerciseAggregation? exerciseAggregation = await _aggregationRepository.GetExerciseAggregationById(aggregationIds, message.UserId, cancellationToken);
        if (exerciseAggregation == null)
        {
            exerciseAggregation = new ExerciseAggregation()
            {
                AggregationId = AggregationIds.Monthly(message.UserId, message.PlayDate.Date),
                Type = AggregationTypeName,
                UserId = message.UserId,
                Year = message.PlayDate.Year,
                Month = message.PlayDate.Month
            };
        }

        if (message.OldRecords != null)
        {
            foreach (var oldRecord in message.OldRecords)
            {
                exerciseAggregation.TotalPlayed -= oldRecord.PlayDuration;

                RollBackByPlayType(oldRecord, exerciseAggregation);
                RollBackByRecordType(oldRecord, exerciseAggregation);
            }
        }

        if (message.NewRecords != null)
        {
            foreach (var newRecord in message.NewRecords)
            {
                exerciseAggregation.TotalPlayed += newRecord.PlayDuration;

                AggregateByPlayType(newRecord, exerciseAggregation);
                AggregateByType(newRecord, exerciseAggregation);
            }
        }

        await _aggregationRepository.UpsertExerciseAggregation(message.UserId, exerciseAggregation, cancellationToken);
    }

    private void AggregateByPlayType(AggregationRecord aggregationRecord, ExerciseAggregation exerciseAggregation)
    {
        if (aggregationRecord.RecordType != RecordType.Song)
            return;

        int playDuration = aggregationRecord.PlayDuration;
        int playedTimes = 1;

        switch (aggregationRecord.PlayType)
        {
            case PlayType.Both:
                {
                    if (exerciseAggregation.BothAggregation != null)
                    {
                        exerciseAggregation.BothAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.BothAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.BothAggregation =
                            new ByPlayTypeAggregation(PlayType.Both.ToString(), playedTimes, playDuration);
                    }
                        
                    break;
                }
            case PlayType.Rhythm:
                {
                    if (exerciseAggregation.RhythmAggregation != null)
                    {
                        exerciseAggregation.RhythmAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.RhythmAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.RhythmAggregation =
                            new ByPlayTypeAggregation(PlayType.Rhythm.ToString(), playedTimes, playDuration);
                    }
                        
                    break;
                }
            case PlayType.Solo:
                {
                    if (exerciseAggregation.SoloAggregation != null)
                    {
                        exerciseAggregation.SoloAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.SoloAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.SoloAggregation =
                            new ByPlayTypeAggregation(PlayType.Solo.ToString(), playedTimes, playDuration);
                    }
                    
                    break;
                }
        }
    }

    private void AggregateByType(AggregationRecord aggregationRecord, ExerciseAggregation exerciseAggregation)
    {
        int playDuration = aggregationRecord.PlayDuration;
        int playedTimes = 1;

        switch (aggregationRecord.RecordType)
        {
            case RecordType.Warmup:
                {
                    if (exerciseAggregation.WarmupAggregation != null)
                    {
                        exerciseAggregation.WarmupAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.WarmupAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.WarmupAggregation =
                            new ByRecordTypeAggregation(RecordType.Warmup.ToString(), playedTimes, playDuration);
                    }
                    
                    break;
                }
            case RecordType.Song:
                {
                    if (exerciseAggregation.SongAggregation != null)
                    {
                        exerciseAggregation.SongAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.SongAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.SongAggregation =
                            new ByRecordTypeAggregation(RecordType.Song.ToString(), playedTimes, playDuration);
                    }

                    break;
                }
            case RecordType.Exercise:
                {
                    if (exerciseAggregation.PracticalExerciseAggregation != null)
                    {
                        exerciseAggregation.PracticalExerciseAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.PracticalExerciseAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.PracticalExerciseAggregation =
                            new ByRecordTypeAggregation(RecordType.Warmup.ToString(), playedTimes, playDuration);
                    }
                        
                    break;
                }
            case RecordType.Composing:
                {
                    if (exerciseAggregation.ComposingAggregation != null)
                    {
                        exerciseAggregation.ComposingAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.ComposingAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.ComposingAggregation =
                            new ByRecordTypeAggregation(RecordType.Composing.ToString(), playedTimes, playDuration);
                    }
                        
                    break;
                }
            case RecordType.Improvisation:
                {
                    if (exerciseAggregation.ComposingAggregation != null)
                    {
                        exerciseAggregation.ComposingAggregation.TotalPlayed += playDuration;
                        exerciseAggregation.ComposingAggregation.TimesPlayed++;
                    }
                    else
                    {
                        exerciseAggregation.ImprovisationAggregation =
                            new ByRecordTypeAggregation(RecordType.Improvisation.ToString(), playedTimes, playDuration);
                    }
                        
                    break;
                }
        }
    }

    private void RollBackByPlayType(AggregationRecord aggregationRecord, ExerciseAggregation exerciseAggregation)
    {
        if (aggregationRecord.RecordType != RecordType.Song)
            return;

        switch (aggregationRecord.PlayType)
        {
            case PlayType.Rhythm:
                {
                    exerciseAggregation.RhythmAggregation!.TimesPlayed--;
                    exerciseAggregation.RhythmAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.RhythmAggregation.TotalPlayed == 0)
                        exerciseAggregation.RhythmAggregation = null;
                    break;
                }
            case PlayType.Solo:
                {
                    exerciseAggregation.SoloAggregation!.TimesPlayed--;
                    exerciseAggregation.SoloAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.SoloAggregation.TotalPlayed == 0)
                        exerciseAggregation.SoloAggregation = null;
                    break;
                }
            case PlayType.Both:
                {
                    exerciseAggregation.BothAggregation!.TimesPlayed--;
                    exerciseAggregation.BothAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.BothAggregation.TotalPlayed == 0)
                        exerciseAggregation.BothAggregation = null;
                    break;
                }
        }
    }

    private void RollBackByRecordType(AggregationRecord aggregationRecord, ExerciseAggregation exerciseAggregation)
    {
        switch (aggregationRecord.RecordType)
        {
            case RecordType.Warmup:
                {
                    exerciseAggregation.WarmupAggregation!.TimesPlayed--;
                    exerciseAggregation.WarmupAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.WarmupAggregation.TotalPlayed == 0)
                        exerciseAggregation.WarmupAggregation = null;
                    break;
                }
            case RecordType.Song:
                {
                    exerciseAggregation.SongAggregation!.TimesPlayed--;
                    exerciseAggregation.SongAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.SongAggregation.TotalPlayed == 0)
                        exerciseAggregation.SongAggregation = null;
                    break;
                }
            case RecordType.Exercise:
                {
                    exerciseAggregation.PracticalExerciseAggregation!.TimesPlayed--;
                    exerciseAggregation.PracticalExerciseAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.PracticalExerciseAggregation.TotalPlayed == 0)
                        exerciseAggregation.PracticalExerciseAggregation = null;
                    break;
                }
            case RecordType.Composing:
                {
                    exerciseAggregation.ComposingAggregation!.TimesPlayed--;
                    exerciseAggregation.ComposingAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.ComposingAggregation.TotalPlayed == 0)
                        exerciseAggregation.ComposingAggregation = null;
                    break;
                }
            case RecordType.Improvisation:
                {
                    exerciseAggregation.ImprovisationAggregation!.TimesPlayed--;
                    exerciseAggregation.ImprovisationAggregation.TotalPlayed -= aggregationRecord.PlayDuration;

                    if (exerciseAggregation.ImprovisationAggregation.TotalPlayed == 0)
                        exerciseAggregation.ImprovisationAggregation = null;
                    break;
                }
        }
    }
}

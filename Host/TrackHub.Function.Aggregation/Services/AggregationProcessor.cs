using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Enums;
using TrackHub.Domain.Repositories;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Services;

internal class AggregationProcessor : IAggregationProcessor
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IAggregationRepository _aggregationRepository;
    
    public AggregationProcessor(IExerciseRepository exerciseRepository, IAggregationRepository aggregationRepository)
    {
        _exerciseRepository = exerciseRepository;
        _aggregationRepository = aggregationRepository;
    }

    public async Task Process(AggregationEventMessage message, CancellationToken cancellationToken)
    {
        ExerciseAggregation exerciseAggregation = new ExerciseAggregation();


        foreach (var item in message.AggregatedRecordStates)
        {
            int playDuration = item.NewRecord.PlayDuration;
            int playedTimes = 1;

            switch (item.NewRecord.RecordType)
            {
                case RecordType.Warmup:
                    {
                        exerciseAggregation.WarmupAggregation =
                            new ByRecordTypeAggregation(RecordType.Warmup.ToString(), playedTimes, playDuration);
                        break;
                    }
                case RecordType.Exercise:
                    {
                        exerciseAggregation.PracticalExerciseAggregation =
                            new ByRecordTypeAggregation(RecordType.Warmup.ToString(), playedTimes, playDuration);
                        break;
                    }
                case RecordType.Composing:
                    {
                        exerciseAggregation.ComposingAggregation =
                         new ByRecordTypeAggregation(RecordType.Composing.ToString(), playedTimes, playDuration);
                        break;
                    }
                case RecordType.Improvisation:
                    {
                        exerciseAggregation.ImprovisationAggregation =
                         new ByRecordTypeAggregation(RecordType.Improvisation.ToString(), playedTimes, playDuration);
                        break;
                    }
                case RecordType.Song:
                    {
                        exerciseAggregation.SongAggregation =
                         new ByRecordTypeAggregation(RecordType.Song.ToString(), playedTimes, playDuration);
                        break;
                    }
            }
        }

        await _aggregationRepository.UpsertAggregation(exerciseAggregation, cancellationToken);
    }        
}

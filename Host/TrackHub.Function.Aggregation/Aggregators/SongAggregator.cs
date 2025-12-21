using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Enums;
using TrackHub.Domain.Repositories;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Aggregators;

internal class SongAggregator : ISongAggregator
{
    private const string AggregationTypeName = "song_aggregation";

    private readonly IAggregationRepository _aggregationRepository;

    private Dictionary<string, SongAggregation> _aggregationsCache = new Dictionary<string, SongAggregation>();

    public SongAggregator(IAggregationRepository aggregationRepository)
    {
        _aggregationRepository = aggregationRepository;
    }

    public async Task AggregateSong(AggregationEventMessage message, CancellationToken cancellationToken)
    {
        if (message.OldRecords != null)
        {
            foreach (var song in message.OldRecords.Where(x => x.RecordType == RecordType.Song))
            {
                var aggregationIds = AggregationIds.Song(message.UserId, song.Author!, song.Name!);

                SongAggregation? songAggregation = await _aggregationRepository.GetSongAggregationById(aggregationIds, message.UserId, cancellationToken);                
                if (songAggregation == null)
                {
                    break;
                }
                else if (songAggregation.TimesPlayed == 0)
                {
                    _aggregationsCache[aggregationIds] = songAggregation;

                    break;
                }
                else
                {
                    songAggregation.TotalPlayed -= song.PlayDuration;
                    songAggregation.TimesPlayed -= 1;

                    var songByDate = songAggregation.SongsByDateAggregations!
                        .FirstOrDefault(x => x.Year == message.PlayDate.Year && x.Month == message.PlayDate.Month);

                    if (songByDate != null)
                    {
                        songByDate.TotalPlayed -= 1;
                        songByDate.TotalDuration -= song.PlayDuration;                        
                    }

                    await _aggregationRepository.UpsertSongAggregation(message.UserId, songAggregation, cancellationToken);
                }
            }
        }
        if (message.NewRecords != null)
        {
            foreach (var song in message.NewRecords.Where(x => x.RecordType == RecordType.Song))
            {

            }
        }
    }
}


/*
 *                     songAggregation = new SongAggregation()
                    {
                        AggregationId = aggregationIds,
                        Type = AggregationTypeName,
                        UserId = message.UserId,
                        Author = song.Author!,
                        Name = song.Name!
                    };
*/
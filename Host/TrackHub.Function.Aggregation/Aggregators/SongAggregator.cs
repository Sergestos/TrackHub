using TrackHub.Domain.Aggregations;
using TrackHub.Domain.Consistency;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;
using TrackHub.Domain.Repositories;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation.Aggregators;

internal class SongAggregator : ISongAggregator
{
    private const string AggregationTypeName = "song_aggregation";

    private readonly IAggregationRepository _aggregationRepository;
    private readonly IUserRepository _userRepository;

    private Dictionary<string, SongAggregation> _aggregationsCache = new Dictionary<string, SongAggregation>();
    private Dictionary<string, SongAggregation> _songsToUpdate = new Dictionary<string, SongAggregation>();

    public SongAggregator(IAggregationRepository aggregationRepository, IUserRepository userRepository)
    {
        _aggregationRepository = aggregationRepository;
        _userRepository = userRepository;
    }

    public async Task AggregateSong(AggregationEventMessage message, CancellationToken cancellationToken)
    {
        if (message.OldRecords != null)
            await RollBackOldRecordsAsync(message.OldRecords, message.UserId, message.PlayDate, cancellationToken);
        if (message.NewRecords != null)
            await AggregateRecordsAsync(message.NewRecords, message.UserId, message.PlayDate, cancellationToken);

        await _aggregationRepository.UpsertSongAggregations(message.UserId, _songsToUpdate.Values.ToArray(), cancellationToken);
        await RecalculateUserPlayedSongsAsync(message.UserId, cancellationToken);
    }

    private async Task RollBackOldRecordsAsync(AggregationRecord[] aggregationRecords, string userId, DateTime playDate, CancellationToken cancellationToken)
    {
        foreach (var song in aggregationRecords.Where(x => x.RecordType == RecordType.Song))
        {
            var aggregationId = AggregationIds.Song(userId, song.Author!, song.Name!);

            SongAggregation? songAggregation = await _aggregationRepository.GetSongAggregationById(aggregationId, userId, cancellationToken);
            if (songAggregation is null)
            {
                break;
            }
            else if (songAggregation.TimesPlayed == 0)
            {
                _aggregationsCache[aggregationId] = songAggregation;
                break;
            }
            else
            {
                songAggregation.TotalPlayed -= song.PlayDuration;
                songAggregation.TimesPlayed--;

                var songByDate = songAggregation.SongsByDateAggregations!
                    .FirstOrDefault(x => x.Year == playDate.Year && x.Month == playDate.Month);

                if (songByDate != null)
                {
                    songByDate.TotalDuration -= song.PlayDuration;
                    songByDate.TimesPlayed--;
                }

                _songsToUpdate[aggregationId] = songAggregation;
                _aggregationsCache[aggregationId] = songAggregation;
            }
        }
    }

    private async Task AggregateRecordsAsync(AggregationRecord[] aggregationRecords, string userId, DateTime playDate, CancellationToken cancellationToken)
    {
        foreach (var song in aggregationRecords.Where(x => x.RecordType == RecordType.Song))
        {
            var aggregationId = AggregationIds.Song(userId, song.Author!, song.Name!);

            SongAggregation? songAggregation;
            if (!_aggregationsCache.ContainsKey(aggregationId))
                songAggregation = await _aggregationRepository.GetSongAggregationById(aggregationId, userId, cancellationToken);
            else
                songAggregation = _aggregationsCache[aggregationId];

            if (songAggregation is null)
            {
                songAggregation = new SongAggregation()
                {
                    AggregationId = aggregationId,
                    Type = AggregationTypeName,
                    UserId = userId,
                    Author = song.Author!,
                    Name = song.Name!                    
                };
            }

            songAggregation.TotalPlayed += song.PlayDuration;
            songAggregation.TimesPlayed++;

            var songByDate = songAggregation.SongsByDateAggregations!
               .FirstOrDefault(x => x.Year == playDate.Year && x.Month == playDate.Month);

            if (songByDate is null)
            {
                songByDate = new SongsByDateAggregation()
                {
                    Year = playDate.Year,
                    Month = playDate.Month
                };
                songAggregation.SongsByDateAggregations.Add(songByDate);
            }

            songByDate.TotalDuration += song.PlayDuration;
            songByDate.TimesPlayed++;


            if (!_songsToUpdate.ContainsKey(aggregationId))
                _songsToUpdate[aggregationId] = songAggregation;
        }
    }

    private async Task RecalculateUserPlayedSongsAsync(string userId, CancellationToken cancellationToken)
    {
        var storedAggregations = (await _aggregationRepository
            .GetSongAggregationsByUserId(userId, cancellationToken))
            .OrderByDescending(x => x.TotalPlayed);

        var orderedSongs = new List<string>();
        foreach (var item in storedAggregations)        
            orderedSongs.Add(UserSongIds.Transform(item.Author!, item.Name));

        User user = _userRepository.GetUserById(userId)!;
        user.OrderedByDurationPlayedSongs = orderedSongs.ToArray();
        await _userRepository.UpsertAsync(user, cancellationToken);
    }
}

namespace TrackHub.Domain.Repositories;

public interface IRecordRepository
{
    Task<IEnumerable<string>> SearchSongsByNameAsync(string pattern, CancellationToken cancellationToken);

    Task<IEnumerable<string>> SearchAuthorsByNameAsync(string pattern, CancellationToken cancellationToken);
}

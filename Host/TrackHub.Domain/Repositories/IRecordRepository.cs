namespace TrackHub.Domain.Repositories;

public interface IRecordRepository
{
    Task<IEnumerable<string>> SearchSongsByNameAsync(string pattern, int searchSize, string[]? excludeList, CancellationToken cancellationToken);

    Task<IEnumerable<string>> SearchAuthorsByNameAsync(string pattern, int searchSize, string[]? excludeList, CancellationToken cancellationToken);
}

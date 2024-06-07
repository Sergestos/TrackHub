namespace TrackHub.Domain.Repositories;

public interface IRecordRepository
{

    Task<IEnumerable<string>> SearchSongsAsync(string songNamePattern, CancellationToken cancellationToken);

    Task<IEnumerable<string>> SearchAuthorAsync(string authorNamePattern, CancellationToken cancellationToken);
}

namespace TrackHub.Contract;

public interface IRecordQuery
{
    Task<IEnumerable<string>> SeachSongsAsync(string textPattern, CancellationToken cancellationToken);

    Task<IEnumerable<string>> SearchAuthorsAsync(string textPattern, CancellationToken cancellationToken);
}

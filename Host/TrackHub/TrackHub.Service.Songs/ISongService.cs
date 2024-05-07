using TrackHub.Domain;

namespace TrackHub.Service.Exercises;

public interface ISongService
{
    Task<IEnumerable<Song>> SearchAsync(string searchText, CancellationToken cancellationToken);
}

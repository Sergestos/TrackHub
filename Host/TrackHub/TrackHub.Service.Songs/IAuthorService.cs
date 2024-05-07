using TrackHub.Domain.Entities;

namespace TrackHub.Service.Exercises;

public interface IAuthorService
{
    Task<IEnumerable<Author>> SearchAsync(string searchText, CancellationToken cancellationToken);
}

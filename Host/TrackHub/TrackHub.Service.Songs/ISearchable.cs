namespace TrackHub.Service.Exercises;

public interface ISearchable<T>
{
    Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken);
}

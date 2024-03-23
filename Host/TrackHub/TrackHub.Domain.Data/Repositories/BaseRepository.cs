using Cassandra;

namespace TrackHub.Domain.Data.Repositories;

internal abstract class BaseRepository
{
    protected ISession Session { get; }

    public BaseRepository()
    {
        Session = CassandraBuilder.Session!;
    }
}

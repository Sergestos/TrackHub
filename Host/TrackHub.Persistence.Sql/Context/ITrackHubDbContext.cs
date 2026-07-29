using Microsoft.EntityFrameworkCore;
using TrackHub.Domain.Entities;

namespace TrackHub.Persistence.Sql.Context;

public interface ITrackHubDbContext
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
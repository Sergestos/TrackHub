using Microsoft.EntityFrameworkCore;
using TrackHub.Domain.Entities;

namespace TrackHub.Infrastructure.Sql.Context;

public sealed class TrackHubDbContext: DbContext, ITrackHubDbContext
{
    public TrackHubDbContext(DbContextOptions<TrackHubDbContext> options): base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TrackHubDbContext).Assembly);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TrackHub.Persistence.Sql.Context;

namespace TrackHub.Persistence.Sql.Migrations;

public sealed class TrackHubDbContextFactory
    : IDesignTimeDbContextFactory<TrackHubDbContext>
{
    public TrackHubDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("TRACKHUB_SQL_CONNECTION")
            ?? throw new InvalidOperationException(
                "TRACKHUB_SQL_CONNECTION is not configured.");

        var options = new DbContextOptionsBuilder<TrackHubDbContext>()
            .UseSqlServer(
                connectionString,
                sql => sql.MigrationsAssembly("TrackHub.Persistence.Sql.Migrations"))
            .Options;

        return new TrackHubDbContext(options);
    }
}
using TrackHub.Domain.Aggregations;

namespace TrackHub.Service.Services.AggregationServices;

public interface IAggregationService
{
    public Task UpsertDayTrendBarAsync(string userId, DateTime dateTime, CancellationToken cancellationToken); 

    public Task<DaysTrendAggregation> BuildDaysTrendAsync(string userId, CancellationToken cancellationToken);
}

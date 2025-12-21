using TrackHub.Domain.Entities;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Service.Aggregation.Services;

public interface IAggregationService
{    
    void SendAggregationRequestOnCreate(Record[] records, DateTime playDate, string userId);

    void SendAggregationRequestOnUpdate(Record[] newRecords, Record[] oldRecords, string userId, DateTime playDate);

    void SendAggregationRequestOnDelete(Record[] oldRecords, string userId, DateTime playDate);
}

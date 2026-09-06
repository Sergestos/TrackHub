using System.Net.Http.Json;
using TrackHub.Domain.Entities;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Service.Aggregation.Transport;

public sealed class AggregationFunctionClient : IAggregationRequestService
{
    private readonly HttpClient _httpClient;

    public AggregationFunctionClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }    

    public void SendAggregationRequestOnCreate(Record[] records, DateTime playDate, string userId)
    {
        var aggregationMessage = new AggregationEventMessage()
        {
            EventDate = DateTime.UtcNow,
            PlayDate = playDate,
            UserId = userId,
            NewRecords = ToAggregation(records),
            OldRecords = null,
        };

        SendAggregation(aggregationMessage);
    }

    public void SendAggregationRequestOnUpdate(Record[] newRecords, Record[] oldRecords, string userId, DateTime playDate)
    {

        var aggregationMessage = new AggregationEventMessage()
        {
            EventDate = DateTime.UtcNow,
            PlayDate = playDate,
            UserId = userId,
            NewRecords = ToAggregation(newRecords),
            OldRecords = ToAggregation(oldRecords)
        };

        SendAggregation(aggregationMessage);
    }

    public void SendAggregationRequestOnDelete(Record[] oldRecords, string userId, DateTime playDate)
    {
        var aggregationMessage = new AggregationEventMessage()
        {
            EventDate = DateTime.UtcNow,
            PlayDate = playDate,
            UserId = userId,
            NewRecords = null,
            OldRecords = ToAggregation(oldRecords)
        };

        SendAggregation(aggregationMessage);
    }

    private void SendAggregation(AggregationEventMessage payload)
    {
        _ = _httpClient.PostAsJsonAsync(
            "api/aggregation",
            payload,
            CancellationToken.None);
    }

   private AggregationRecord ToAggregation(Record record) => new()
    {
        PlayDuration = record.PlayDuration,
        Author = record.Author,
        Name = record.Name,
        RecordType = record.RecordType,
        PlayType = record.PlayType,
    };

    private AggregationRecord[] ToAggregation(Record[] records) =>
        Array.ConvertAll(records, ToAggregation);
}
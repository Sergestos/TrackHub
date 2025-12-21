using AutoMapper;
using System.Net.Http.Json;
using TrackHub.Domain.Entities;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Service.Aggregation.Services;

public sealed class AggregationFunctionClient : IAggregationService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public AggregationFunctionClient(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }    

    public void SendAggregationRequestOnCreate(Record[] records, DateTime playDate, string userId)
    {
        var aggregationMessage = new AggregationEventMessage()
        {
            EventDate = DateTime.UtcNow,
            PlayDate = playDate,
            UserId = userId,
            NewRecords = _mapper.Map<AggregationRecord[]>(records),
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
            NewRecords = _mapper.Map<AggregationRecord[]>(newRecords),
            OldRecords = _mapper.Map<AggregationRecord[]>(oldRecords)
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
            OldRecords = _mapper.Map<AggregationRecord[]>(oldRecords)
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
}
using System.Net.Http.Json;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Service.Aggregation.Services;

public sealed class AggregationFunctionClient : IAggregationService
{
    private readonly HttpClient _httpClient;

    public AggregationFunctionClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void SendAggregation(AggregationEventMessage payload)
    {
        _ = _httpClient.PostAsJsonAsync(
            "api/aggregation",
            payload,
            CancellationToken.None);
    }
}
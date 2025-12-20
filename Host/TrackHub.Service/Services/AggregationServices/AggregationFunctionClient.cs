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

    public async Task SendAggregationAsync(AggregationEventMessage payload, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/AggregatePlays",
            payload,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
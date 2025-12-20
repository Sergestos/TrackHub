using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TrackHub.Function.Aggregation.Services;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation;

public class AggregationFunction
{
    private readonly ILogger<AggregationFunction> _logger;
    private readonly IAggregationProcessor _aggregationProcessor;

    public AggregationFunction(ILogger<AggregationFunction> logger, IAggregationProcessor aggregationProcessor)
    {
        _logger = logger;
        _aggregationProcessor = aggregationProcessor;
    }

    [Function("aggregation")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Aggregation function started");

        AggregationEventMessage? payload;

        try
        {
            payload = await JsonSerializer.DeserializeAsync<AggregationEventMessage>(
                request.Body, 
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            await _aggregationProcessor.Process(payload!, cancellationToken);
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Invalid JSON payload");
            return new BadRequestObjectResult("Invalid JSON payload");
        }

        if (payload is null)
            return new BadRequestObjectResult("Payload is required");

        _logger.LogInformation("Processed aggregation event for user " + payload.UserId);

        return new OkObjectResult("Aggregation received");
    }
}
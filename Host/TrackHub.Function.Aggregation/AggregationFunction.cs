using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TrackHub.Function.Aggregation.Aggregators;
using TrackHub.Messaging.Aggregations;

namespace TrackHub.Function.Aggregation;

public class AggregationFunction
{
    private readonly ILogger<AggregationFunction> _logger;
    private readonly IExerciseAggregator _exerciseAggregator;
    private readonly ISongAggregator _songAggregator;

    public AggregationFunction(ILogger<AggregationFunction> logger, IExerciseAggregator exerciseAggregator, ISongAggregator songAggregator)
    {
        _logger = logger;
        _exerciseAggregator = exerciseAggregator;
        _songAggregator = songAggregator;
    }

    [Function("aggregation")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest request, CancellationToken cancellationToken)
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

            await _exerciseAggregator.AggregateExercise(payload!, cancellationToken);
       //     await _songAggregator.AggregateSong(payload!, cancellationToken);
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TrackHub.Function.Aggregation;

public class AggregationFunction
{
    private readonly ILogger<AggregationFunction> _logger;

    public AggregationFunction(ILogger<AggregationFunction> logger)
    {
        _logger = logger;
    }

    [Function("aggregation")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
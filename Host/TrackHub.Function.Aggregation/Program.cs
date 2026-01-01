using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrackHub.Domain.Data;
using TrackHub.Function.Aggregation.Aggregators;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights()
    .Configure<CosmosClientOptions>(builder.Configuration.GetSection("CosmosDb"))
    .AddScoped<IExerciseAggregator, ExerciseAggregator>()
    .AddScoped<ISongAggregator, SongAggregator>()
    .AddDataServices(builder.Configuration);

builder.Build().Run();

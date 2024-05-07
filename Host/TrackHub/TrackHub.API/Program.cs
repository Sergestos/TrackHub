using TrackHub.ExerciseService;
using TrackHub.Domain.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CosmosClientOptions>(builder.Configuration.GetSection("CosmosDb"));

builder.Services.AddExerciseServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

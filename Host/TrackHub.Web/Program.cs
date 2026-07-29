using TrackHub.AiCrawler;
using TrackHub.Persistence;
using TrackHub.Service.Exercises;
using TrackHub.Application.Service.Preview;
using TrackHub.Application.Service.User;
using TrackHub.Service.Scraper;
using TrackHub.Web.Configurations;
using TrackHub.Web.Mappers;
using TrackHub.Persistence.CosmosDb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cgf => { }, typeof(AppMapper));

builder.Services.Configure<CosmosClientOptions>(builder.Configuration.GetSection("CosmosDb"));
builder.Services.AddSqlDataServices(builder.Configuration.GetSection("sqlConnectionString").Value!);
builder.Services.AddDataServices();
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddScraperServices();
builder.Services.AddPreviewServices();
builder.Services.AddUserServices();
builder.Services.AddAiCrawlerServices();
builder.Services.AddCommonServices();
builder.Services.AddCorsPolicy();
builder.Services.AddRateLimitConfiguration();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseRateLimiter();
app.AddErrorHandling();
app.MapControllers();
app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
  

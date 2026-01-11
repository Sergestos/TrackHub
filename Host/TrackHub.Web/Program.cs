using TrackHub.AiCrawler;
using TrackHub.Domain.Data;
using TrackHub.Service;
using TrackHub.Service.Scraper;
using TrackHub.Web.Configurations;
using TrackHub.Web.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AppMapper));

builder.Services.Configure<CosmosClientOptions>(builder.Configuration.GetSection("CosmosDb"));
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddScraperServices();
builder.Services.AddAiCrawlerServices();
builder.Services.AddCommonServices(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.Services.AddRateLimiter();
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
  
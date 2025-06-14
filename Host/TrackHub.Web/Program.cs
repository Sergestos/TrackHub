using TrackHub.Domain.Data;
using TrackHub.Service;
using TrackHub.AiCrawler;
using TrackHub.Web.Configurations;
using TrackHub.Web.Mappers;
using TrackHub.Scraper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AppMapper));

builder.Services.Configure<CosmosClientOptions>(builder.Configuration.GetSection("CosmosDb"));
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddScraperServices();
builder.Services.AddAiCrawlerServices();
builder.Services.AddCommonServices();

builder.Services.AddCorsPolicy();
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
app.MapControllers();
app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

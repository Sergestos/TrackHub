using TrackHub.Domain.Data;
using TrackHub.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CosmosClientOptions>(builder.Configuration.GetSection("CosmosDb"));
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddCorsPolicy();

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
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

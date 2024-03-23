using TrackHub.Domain.Data;
using TrackHub.Service.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCassandraDb(builder.Configuration);
builder.Services.AddUserServices();

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

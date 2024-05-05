using StockManagement.Application;
using StockManagement.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddHealthChecks()
    .AddNpgSql(config.GetConnectionString("DefaultConnection")!);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service registration layer
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});/*.RequireAuthorization();*/

app.UseAuthorization();

app.MapControllers();

app.Run();

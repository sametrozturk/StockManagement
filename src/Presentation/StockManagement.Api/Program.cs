using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Quartz;
using Serilog;
using StockManagement.Application;
using StockManagement.Infrastructure.BackgroundJobs;
using StockManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHealthChecks()
    .AddSqlServer(config.GetConnectionString("DefaultConnection")!);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service registration layer
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(config);

builder.Services.AddQuartz(configure =>
{
    var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

    configure
        .AddJob<ProcessOutboxMessagesJob>(jobKey)
        .AddTrigger(
            trigger =>
                trigger.ForJob(jobKey)
                    .WithSimpleSchedule(
                        schedule =>
                            schedule.WithIntervalInSeconds(50)
                                .RepeatForever()));
});

builder.Services.AddQuartzHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});/*.RequireAuthorization();*/

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

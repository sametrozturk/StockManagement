using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Quartz;
using Serilog;
using StockManagement.Application;
using StockManagement.Infrastructure.BackgroundJobs;
using StockManagement.Persistence;
using NSwag;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHealthChecks()
    .AddSqlServer(config.GetConnectionString("DefaultConnection")!);

// Add services to the container.
builder.Services.AddControllers();

// Configure OpenAPI with NSwag
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Stock Management API";
    configure.Version = "v1";
    configure.Description = "API documentation for Stock Management system";

    configure.AddSecurity("JWT", new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    configure.PostProcess = document =>
    {
        document.Info.Contact = new OpenApiContact
        {
            Name = "API Support",
            Email = "support@stockmanagement.com",
            Url = "https://www.stockmanagement.com/contact"
        };
        document.Info.License = new OpenApiLicense
        {
            Name = "MIT",
            Url = "https://opensource.org/licenses/MIT"
        };
    };
});

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
    app.UseOpenApi(); // OpenAPI belgesini oluþtur
    app.UseSwaggerUI(settings =>
    {
        settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        settings.DocumentTitle = "My Custom API Documentation";
    });

}

app.UseHttpsRedirection();

app.MapHealthChecks("/", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
}); // Optional: .RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

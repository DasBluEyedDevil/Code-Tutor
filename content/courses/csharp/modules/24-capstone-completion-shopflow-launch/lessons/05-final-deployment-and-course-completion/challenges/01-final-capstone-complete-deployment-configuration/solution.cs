// Program.cs - Complete production configuration

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog with structured logging
builder.Host.UseSerilog((context, config) =>
{
    config
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.ApplicationInsights(
            context.Configuration["ApplicationInsights:ConnectionString"],
            TelemetryConverter.Traces);
});

// Add health checks for all critical dependencies
builder.Services.AddHealthChecks()
    .AddNpgSql(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "database",
        tags: new[] { "ready" })
    .AddRedis(
        builder.Configuration.GetConnectionString("Redis")!,
        name: "cache",
        tags: new[] { "ready" })
    .AddUrlGroup(
        new Uri("https://api.stripe.com/v1"),
        name: "stripe",
        tags: new[] { "ready", "external" });

// Configure options validation
builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection("Database"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<StripeOptions>()
    .Bind(builder.Configuration.GetSection("Stripe"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddControllers();

var app = builder.Build();

// Liveness probe - just confirms app is running
app.MapHealthChecks("/healthz/live", new HealthCheckOptions
{
    Predicate = _ => false
});

// Readiness probe - verifies all dependencies are available
app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapControllers();
app.Run();

// Configuration classes with validation
public class DatabaseOptions
{
    [Required(ErrorMessage = "Database connection string is required")]
    public string ConnectionString { get; set; } = null!;
}

public class StripeOptions
{
    [Required(ErrorMessage = "Stripe secret key is required")]
    public string SecretKey { get; set; } = null!;
    
    [Required(ErrorMessage = "Stripe webhook secret is required")]
    public string WebhookSecret { get; set; } = null!;
}
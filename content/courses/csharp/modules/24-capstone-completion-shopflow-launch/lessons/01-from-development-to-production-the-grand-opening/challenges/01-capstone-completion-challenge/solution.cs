// Program.cs - Configure production health checks and monitoring

var builder = WebApplication.CreateBuilder(args);

// Configure health checks for all critical dependencies
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
        name: "payment-gateway",
        tags: new[] { "ready" });

// Add Application Insights for production monitoring
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
    options.EnableAdaptiveSampling = true;
});

var app = builder.Build();

// Liveness probe - just confirms the app is running
app.MapHealthChecks("/healthz/live", new HealthCheckOptions
{
    Predicate = _ => false // Skip all checks, just return 200 if app responds
});

// Readiness probe - verifies all dependencies are available
app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
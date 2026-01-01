---
type: "THEORY"
title: "Post-Deployment Monitoring"
---

Deploying to production is just the beginning. Ongoing monitoring ensures your application remains healthy, performant, and available. Proactive monitoring catches issues before customers report them.

## Health Checks

Health check endpoints serve multiple purposes. Container orchestrators use them to route traffic and restart unhealthy instances. Load balancers use them to remove failed nodes from rotation. Operations teams use them to verify deployment success.

```csharp
// Configure health checks with dependencies
builder.Services.AddHealthChecks()
    .AddNpgSql(
        connectionString,
        name: "database",
        tags: new[] { "ready", "db" })
    .AddRedis(
        redisConnectionString,
        name: "redis",
        tags: new[] { "ready", "cache" })
    .AddUrlGroup(
        new Uri("https://api.stripe.com/v1"),
        name: "stripe",
        tags: new[] { "ready", "external" })
    .AddCheck<CustomHealthCheck>("custom");

// Health check UI for dashboards
builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(30);
    opt.AddHealthCheckEndpoint("ShopFlow API", "/healthz/ready");
})
.AddInMemoryStorage();
```

## Structured Logging

Structured logging captures context that helps debug production issues. Rather than searching through text, you can query by order number, user ID, or correlation ID.

```csharp
// Serilog configuration for structured logging
builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName()
        .WriteTo.Console()
        .WriteTo.ApplicationInsights(
            context.Configuration["ApplicationInsights:ConnectionString"],
            TelemetryConverter.Traces);
});

// Usage in handlers
_logger.LogInformation(
    "Order {OrderNumber} placed by user {UserId}. Total: {Total:C}",
    order.OrderNumber,
    userId,
    order.Total.Amount);
```

## Metrics and Alerting

Key metrics for e-commerce include order success rate, checkout abandonment, API response times, and error rates. Configure alerts for anomalies that require immediate attention.

```csharp
// Custom metrics with Application Insights
public class OrderMetrics
{
    private readonly TelemetryClient _telemetry;

    public void RecordOrderPlaced(Order order)
    {
        _telemetry.TrackEvent("OrderPlaced", 
            properties: new Dictionary<string, string>
            {
                ["OrderNumber"] = order.OrderNumber,
                ["ItemCount"] = order.Items.Count.ToString()
            },
            metrics: new Dictionary<string, double>
            {
                ["OrderTotal"] = (double)order.Total.Amount
            });

        _telemetry.GetMetric("OrdersPerMinute").TrackValue(1);
    }

    public void RecordCheckoutAbandoned(int userId, decimal cartValue)
    {
        _telemetry.TrackEvent("CheckoutAbandoned",
            metrics: new Dictionary<string, double>
            {
                ["CartValue"] = (double)cartValue
            });
    }
}
```

Set up alerts in Azure Monitor for critical thresholds: error rate above 1%, average response time above 500ms, or zero successful orders in 10 minutes. These alerts should page on-call engineers for immediate response.

## Performance Baselines

Establish performance baselines during normal operation. When you notice response times increasing or error rates climbing, compare against these baselines to determine severity. Small deviations might indicate gradual degradation that needs investigation. Large spikes indicate immediate problems.

Monitoring is not just about catching failures. It is about understanding your application's behavior, identifying optimization opportunities, and building confidence in your production environment. The investment in monitoring pays dividends every time you deploy a new feature or investigate a customer issue.
---
type: "ARCHITECTURE"
title: "Production Architecture"
---

## Azure Container Apps Deployment

Azure Container Apps provides a managed platform for running containerized applications without managing Kubernetes directly. ShopFlow deploys as multiple containers: the main API, the Blazor frontend, and background workers for order processing. Container Apps handles load balancing, automatic scaling, and SSL termination. Configure minimum and maximum replicas based on expected traffic, allowing the platform to scale horizontally during peak demand.

```yaml
# Container Apps configuration
resources:
  cpu: 0.5
  memory: 1Gi
scale:
  minReplicas: 2
  maxReplicas: 10
  rules:
    - name: http-scaling
      http:
        metadata:
          concurrentRequests: 50
```

## Health Checks Integration

ASP.NET Core health checks integrate directly with container orchestration platforms. The liveness endpoint at /healthz/live returns 200 if the application is running, allowing the platform to restart crashed instances. The readiness endpoint at /healthz/ready verifies database connectivity, cache availability, and external service accessibility before routing traffic to an instance.

```csharp
services.AddHealthChecks()
    .AddNpgSql(connectionString, name: "database")
    .AddRedis(redisConnection, name: "cache")
    .AddUrlGroup(new Uri("https://api.stripe.com/v1"), name: "payment-gateway");

app.MapHealthChecks("/healthz/live", new HealthCheckOptions
{
    Predicate = _ => false // Just check if app responds
});

app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
```

## Application Insights Monitoring

Application Insights provides comprehensive observability for production applications. It automatically captures request telemetry, dependency calls, exceptions, and performance metrics. Configure custom telemetry for business events like orders placed, payments processed, and user registrations. Create dashboards showing key performance indicators and set up alerts for anomalies.

```csharp
services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = configuration["ApplicationInsights:ConnectionString"];
    options.EnableAdaptiveSampling = true; // Reduce cost on high-volume apps
});

// Custom telemetry for business events
public class OrderService
{
    private readonly TelemetryClient _telemetry;
    
    public async Task<Order> CreateOrderAsync(...)
    {
        // ... order creation logic ...
        
        _telemetry.TrackEvent("OrderCreated", new Dictionary<string, string>
        {
            ["OrderId"] = order.Id.ToString(),
            ["Total"] = order.Total.ToString("C"),
            ["ItemCount"] = order.Items.Count.ToString()
        });
    }
}
```

## Azure Key Vault for Secrets

Production secrets must never exist in configuration files or environment variables visible to developers. Azure Key Vault provides secure, audited secret storage. The application authenticates to Key Vault using managed identity, eliminating the need for any credentials in configuration. Key Vault integration loads secrets at startup and can reload changed values without restart.

```csharp
builder.Configuration.AddAzureKeyVault(
    new Uri($"https://{keyVaultName}.vault.azure.net/"),
    new DefaultAzureCredential());

// Secrets are now available as configuration values
var stripeKey = configuration["Stripe:SecretKey"];
var dbConnection = configuration["Database:ConnectionString"];
```

## Scaling Strategies

ShopFlow uses different scaling strategies for different components. The stateless API scales horizontally based on concurrent requests, with Container Apps adding replicas during traffic spikes. The database uses connection pooling to handle increased load without scaling, with read replicas available for query-heavy scenarios. Background workers scale based on queue depth, processing more order confirmations during sales events. Redis cache scales vertically by increasing memory, or horizontally with clustering for extreme scale.

## Blue-Green Deployment

Zero-downtime deployments use blue-green strategy: the new version deploys alongside the current version. Health checks verify the new deployment is healthy before switching traffic. If issues arise, traffic reverts to the previous version instantly. Container Apps supports revision management for this pattern, maintaining multiple versions simultaneously and controlling traffic distribution between them.

```bash
# Deploy new revision
az containerapp update --name shopflow-api \
  --resource-group shopflow-prod \
  --image shopflowacr.azurecr.io/api:v2.1.0

# Verify health, then switch traffic
az containerapp ingress traffic set --name shopflow-api \
  --resource-group shopflow-prod \
  --revision-weight latest=100
```
---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates .NET Aspire in action.

```csharp
// === APPHOST PROJECT (Orchestrator) ===
// MyApp.AppHost/Program.cs

var builder = DistributedApplication.CreateBuilder(args);

// Add a Redis cache
var cache = builder.AddRedis("cache");

// Add PostgreSQL database
var postgres = builder.AddPostgres("postgres")
    .AddDatabase("ordersdb");

// Add API service (references cache + db)
var api = builder.AddProject<Projects.OrdersApi>("orders-api")
    .WithReference(cache)
    .WithReference(postgres);

// Add Blazor frontend (references API)
builder.AddProject<Projects.WebFrontend>("web-frontend")
    .WithReference(api);

builder.Build().Run();

// === SERVICE DEFAULTS ===
// MyApp.ServiceDefaults/Extensions.cs
public static IHostApplicationBuilder AddServiceDefaults(
    this IHostApplicationBuilder builder)
{
    // OpenTelemetry (automatic tracing!)
    builder.ConfigureOpenTelemetry();
    
    // Health checks
    builder.AddDefaultHealthChecks();
    
    // Service discovery
    builder.Services.AddServiceDiscovery();
    
    return builder;
}

// === API PROJECT ===
// OrdersApi/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults
builder.AddServiceDefaults();

// Add Redis cache (Aspire handles connection!)
builder.AddRedisClient("cache");

// Add PostgreSQL via EF Core
builder.AddNpgsqlDbContext<OrdersDbContext>("ordersdb");

var app = builder.Build();

app.MapDefaultEndpoints(); // Health checks
app.MapGet("/orders", async (OrdersDbContext db) =>
    await db.Orders.ToListAsync());

app.Run();

// === BENEFITS ===
// 1. No connection strings! Aspire injects them
// 2. Service discovery: Use "orders-api" as hostname
// 3. Dashboard: See all logs, traces, metrics
// 4. Health checks: Automatic
// 5. Telemetry: Built-in OpenTelemetry
```

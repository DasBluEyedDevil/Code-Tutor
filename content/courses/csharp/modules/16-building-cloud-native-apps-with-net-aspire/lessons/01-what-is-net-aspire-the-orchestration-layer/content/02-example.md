---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== PROJECT STRUCTURE =====
// MyApp.sln
//   MyApp.AppHost/        <- The orchestrator (conductor)
//   MyApp.ServiceDefaults/ <- Shared configuration
//   MyApp.Api/             <- Your API service
//   MyApp.Web/             <- Your web frontend

// ===== AppHost/Program.cs =====
// This is the HEART of .NET Aspire - defines your entire app!

var builder = DistributedApplication.CreateBuilder(args);

// Add infrastructure components
var cache = builder.AddRedis("cache");
var db = builder.AddPostgres("postgres")
    .AddDatabase("catalogdb");

// Add your API project with references to infrastructure
var api = builder.AddProject<Projects.CatalogApi>("api")
    .WithReference(cache)    // API can access Redis
    .WithReference(db);      // API can access Postgres

// Add web frontend that talks to the API
builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(api);     // Web can call API

builder.Build().Run();

// ===== WHAT ASPIRE DOES FOR YOU =====
// 1. Starts Redis container automatically
// 2. Starts Postgres container automatically
// 3. Configures connection strings
// 4. Sets up service discovery (webapp knows api URL)
// 5. Launches dashboard at http://localhost:18888

// ===== ServiceDefaults/Extensions.cs =====
public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(
        this IHostApplicationBuilder builder)
    {
        // OpenTelemetry for logging, metrics, traces
        builder.ConfigureOpenTelemetry();
        
        // Health checks
        builder.AddDefaultHealthChecks();
        
        // Service discovery
        builder.Services.AddServiceDiscovery();
        
        // Resilient HTTP clients
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });
        
        return builder;
    }
}

// ===== In your API's Program.cs =====
var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults (one line!)
builder.AddServiceDefaults();

// Add Redis cache (connection string auto-configured!)
builder.AddRedisClient("cache");

// Add database (connection string auto-configured!)
builder.AddNpgsqlDbContext<CatalogDbContext>("catalogdb");

var app = builder.Build();
app.MapDefaultEndpoints();  // Health checks, etc.
app.Run();

Console.WriteLine(".NET Aspire orchestrates your distributed app!");
Console.WriteLine("Dashboard at: http://localhost:18888");
Console.WriteLine("Run: dotnet run --project MyApp.AppHost");
```

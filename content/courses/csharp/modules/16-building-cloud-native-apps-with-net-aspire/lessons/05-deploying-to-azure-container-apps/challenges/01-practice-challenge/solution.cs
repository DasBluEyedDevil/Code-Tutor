// Production-Ready AppHost for Azure Container Apps

var builder = DistributedApplication.CreateBuilder(args);

// ===== INFRASTRUCTURE =====
// Redis cache - maps to Azure Cache for Redis (managed)
var cache = builder.AddRedis("appcache");

// PostgreSQL - maps to Azure Database for PostgreSQL (managed)
var db = builder.AddPostgres("postgres")
    .AddDatabase("appdb");

// ===== SERVICES =====

// Background worker - INTERNAL only (no WithExternalHttpEndpoints)
// Processes jobs from database, updates cache
// Maps to: Azure Container App (internal ingress only)
var worker = builder.AddProject<Projects.BackgroundWorker>("worker")
    .WithReference(cache)
    .WithReference(db);

// API service - EXTERNAL (public internet access)
// Serves REST endpoints for web and mobile clients
// Maps to: Azure Container App (external ingress + public URL)
var api = builder.AddProject<Projects.PublicApi>("api")
    .WithReference(cache)
    .WithReference(db)
    .WithExternalHttpEndpoints();  // Creates public URL

// Web frontend - EXTERNAL (public internet access)
// Server-side rendered web app, calls API via service discovery
// Maps to: Azure Container App (external ingress + public URL)
builder.AddProject<Projects.WebFrontend>("web")
    .WithReference(api)  // Service discovery to API
    .WithExternalHttpEndpoints();  // Creates public URL

builder.Build().Run();

// Print deployment guide
Console.WriteLine("AppHost configured for Azure Container Apps!");
Console.WriteLine("");
Console.WriteLine("Infrastructure (managed Azure services):");
Console.WriteLine("  - appcache -> Azure Cache for Redis");
Console.WriteLine("  - appdb    -> Azure Database for PostgreSQL");
Console.WriteLine("");
Console.WriteLine("Services (Azure Container Apps):");
Console.WriteLine("  - worker   -> Internal (background processing)");
Console.WriteLine("  - api      -> External (public REST API)");
Console.WriteLine("  - web      -> External (public website)");
Console.WriteLine("");
Console.WriteLine("Deploy with:");
Console.WriteLine("  1. azd init");
Console.WriteLine("  2. azd up");
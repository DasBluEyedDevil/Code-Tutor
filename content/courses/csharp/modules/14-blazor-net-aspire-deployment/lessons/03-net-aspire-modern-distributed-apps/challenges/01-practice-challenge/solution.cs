Console.WriteLine("=== .NET ASPIRE ARCHITECTURE ===");

Console.WriteLine("\n--- APPHOST PROJECT (Orchestrator) ---");
Console.WriteLine(@"
// MyStore.AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var cache = builder.AddRedis(""cache"");
var sql = builder.AddSqlServer(""sql"")
    .AddDatabase(""catalogdb"");

// Services
var catalogApi = builder.AddProject<Projects.CatalogApi>(""catalog-api"")
    .WithReference(cache)
    .WithReference(sql);

// Frontend
builder.AddProject<Projects.WebStore>(""web-store"")
    .WithReference(catalogApi);

builder.Build().Run();
");

Console.WriteLine("\n--- CATALOG API PROJECT ---");
Console.WriteLine(@"
// CatalogApi/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Aspire defaults (telemetry, health, discovery)
builder.AddServiceDefaults();

// Database (connection injected by Aspire!)
builder.AddSqlServerDbContext<CatalogContext>(""catalogdb"");

// Cache (connection injected by Aspire!)
builder.AddRedisClient(""cache"");

var app = builder.Build();

// Health endpoints for dashboard
app.MapDefaultEndpoints();

// API endpoints
app.MapGet(""/products"", async (CatalogContext db) =>
    await db.Products.ToListAsync());

app.Run();
");

Console.WriteLine("\n--- WEB STORE FRONTEND ---");
Console.WriteLine(@"
// WebStore/Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// HttpClient with service discovery!
builder.Services.AddHttpClient<CatalogClient>(client =>
{
    // ""catalog-api"" resolved by Aspire!
    client.BaseAddress = new Uri(""http://catalog-api"");
});

builder.Build().Run();
");

Console.WriteLine("\n=== BENEFITS vs DOCKER-COMPOSE ===");
Console.WriteLine("DOCKER-COMPOSE:");
Console.WriteLine("  - Write YAML by hand");
Console.WriteLine("  - Configure networks manually");
Console.WriteLine("  - Hardcode connection strings");
Console.WriteLine("  - No observability dashboard");
Console.WriteLine("  - Debug with 'docker logs' commands");

Console.WriteLine("\n.NET ASPIRE:");
Console.WriteLine("  + Write C# code");
Console.WriteLine("  + Automatic networking");
Console.WriteLine("  + Connection strings injected");
Console.WriteLine("  + Beautiful dashboard (logs, traces, metrics)");
Console.WriteLine("  + Click-to-debug in Visual Studio");
Console.WriteLine("  + OpenTelemetry built-in");
Console.WriteLine("  + Service discovery: just use service names!");

Console.WriteLine("\n.NET Aspire is the MODERN way to build distributed apps!");
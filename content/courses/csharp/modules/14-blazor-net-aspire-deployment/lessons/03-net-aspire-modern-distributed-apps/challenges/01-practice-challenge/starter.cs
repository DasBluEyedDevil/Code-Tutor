Console.WriteLine("=== .NET ASPIRE ARCHITECTURE ===");

Console.WriteLine("\n--- APPHOST PROJECT (Orchestrator) ---");
Console.WriteLine(@"
var builder = DistributedApplication.CreateBuilder(args);

// TODO: Add Redis cache
// TODO: Add SQL Server
// TODO: Add catalog-api with references
// TODO: Add web-store frontend

builder.Build().Run();
");

Console.WriteLine("\n--- CATALOG API PROJECT ---");
// TODO: Show API setup with Aspire

Console.WriteLine("\n--- BENEFITS vs DOCKER-COMPOSE ---");
// TODO: List benefits
// Program.cs - Configure production health checks and monitoring

var builder = WebApplication.CreateBuilder(args);

// TODO: Add health checks for:
// - PostgreSQL database (connection string: builder.Configuration.GetConnectionString("DefaultConnection"))
// - Redis cache (connection string: builder.Configuration.GetConnectionString("Redis"))
// - Payment API (URL: "https://api.stripe.com/v1")

// TODO: Add Application Insights telemetry

var app = builder.Build();

// TODO: Map health check endpoints
// - /healthz/live - Liveness probe (no dependency checks)
// - /healthz/ready - Readiness probe (check all dependencies tagged with "ready")

app.Run();
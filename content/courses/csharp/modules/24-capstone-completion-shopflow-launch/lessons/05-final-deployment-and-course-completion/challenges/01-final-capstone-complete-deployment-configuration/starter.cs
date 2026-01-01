// Program.cs - Complete production configuration

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// TODO: Configure Serilog with structured logging
// - Add enrichers for environment and machine name
// - Write to Console
// - Write to Application Insights (use configuration for connection string)

// TODO: Add health checks for:
// - PostgreSQL (name: database, tag: ready)
// - Redis (name: cache, tag: ready)
// - Stripe API at https://api.stripe.com/v1 (name: stripe, tags: ready, external)

// TODO: Configure options validation for:
// - DatabaseOptions (bind to "Database" section, validate annotations, validate on start)
// - StripeOptions (bind to "Stripe" section, validate annotations, validate on start)

var app = builder.Build();

// TODO: Map health check endpoints
// - /healthz/live: Liveness probe with Predicate = _ => false
// - /healthz/ready: Readiness probe filtering for "ready" tag

app.MapControllers();
app.Run();

// Configuration classes
public class DatabaseOptions
{
    // TODO: Add Required attribute and ConnectionString property
}

public class StripeOptions
{
    // TODO: Add Required attributes for SecretKey and WebhookSecret
}
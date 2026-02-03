---
type: "KEY_POINT"
title: ".NET Aspire Orchestration"
---

## Key Takeaways

- **Aspire orchestrates all your services from one C# project** -- `builder.AddProject<T>("name")` registers services. `builder.AddRedis("cache")` adds infrastructure. One `dotnet run` starts everything.

- **`.WithReference()` wires services together** -- connection strings and service URLs are injected automatically. No more manual configuration in appsettings.json for local development.

- **`AddServiceDefaults()` provides production-ready observability** -- OpenTelemetry, health checks, and service discovery are configured with a single line in each service project.

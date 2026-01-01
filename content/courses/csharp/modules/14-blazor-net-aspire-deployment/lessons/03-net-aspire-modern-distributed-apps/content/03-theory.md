---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`DistributedApplication.CreateBuilder()`**: Creates Aspire orchestrator. This is like WebApplication.CreateBuilder() but for multiple services.

**`builder.AddProject<T>("name")`**: Adds a .NET project to orchestration. The name becomes the service discovery hostname.

**`.WithReference()`**: Connects services. Aspire injects connection strings automatically. No more appsettings.json connection string juggling!

**`builder.AddRedis/Postgres()`**: Adds containers automatically. Aspire downloads and runs them. In production, swap to Azure/AWS versions.

**`AddServiceDefaults()`**: Shared configuration. Adds OpenTelemetry, health checks, service discovery. Every service gets consistent observability.
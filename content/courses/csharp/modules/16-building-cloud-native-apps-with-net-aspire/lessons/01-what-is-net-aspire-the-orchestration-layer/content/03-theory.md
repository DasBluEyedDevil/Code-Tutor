---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`DistributedApplication.CreateBuilder(args)`**: Entry point for Aspire. Creates an orchestrator that manages all your services and infrastructure.

**`builder.AddRedis("cache")`**: Adds a Redis container to your app. The name 'cache' becomes the connection string name. Aspire handles container lifecycle automatically.

**`builder.AddPostgres("postgres").AddDatabase("catalogdb")`**: Adds Postgres server and creates a database. Chained calls configure complex infrastructure simply.

**`builder.AddProject<Projects.CatalogApi>("api")`**: Adds a .NET project to orchestration. 'Projects.CatalogApi' is auto-generated from your solution. Name 'api' is used for service discovery.

**`.WithReference(cache)`**: Creates a dependency. The API project will receive the Redis connection string automatically. Service discovery 'just works'.

**`builder.AddServiceDefaults()`**: In each service, adds OpenTelemetry, health checks, service discovery, and resilient HTTP clients. One line for production-ready defaults.
---
type: "KEY_POINT"
title: "Application and Infrastructure Layers"
---

## Key Takeaways

- **Application Services orchestrate use cases** -- `CreateOrderService` coordinates domain entities, repository calls, and event publishing. It contains no business rules (those belong in Domain) and no data access (that belongs in Infrastructure).

- **Infrastructure implements interfaces defined in Application** -- `ProductRepository : IProductRepository` uses EF Core. If you switch to Dapper, only Infrastructure changes. Application and Domain remain unchanged.

- **Register Infrastructure services in the Presentation layer** -- `builder.Services.AddScoped<IProductRepository, ProductRepository>()` wires the dependency in Program.cs. This is the only place where all layers connect.

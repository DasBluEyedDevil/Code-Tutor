---
type: "KEY_POINT"
title: "ASP.NET Core Minimal API Setup"
---

## Key Takeaways

- **`WebApplication.CreateBuilder(args)` is your starting point** -- it configures dependency injection, logging, and configuration. Chain `.Build()` to create the app, then map endpoints and call `.Run()`.

- **`app.MapGet("/path", handler)` defines endpoints** -- the handler is a lambda or method that returns data. ASP.NET Core automatically serializes return values to JSON.

- **Use `TypedResults` over `Results`** -- `TypedResults.Ok(data)` provides compile-time type checking and generates accurate OpenAPI documentation automatically. Always prefer the strongly-typed version.

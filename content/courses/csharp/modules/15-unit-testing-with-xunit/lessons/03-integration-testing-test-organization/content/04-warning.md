---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**In-Memory Database Limitations**: EF Core in-memory provider doesn't enforce foreign keys, unique constraints, or transactions like a real database! Use Testcontainers for realistic integration tests.

**Testcontainers for Real Databases**: `dotnet add package Testcontainers.PostgreSql` - spins up actual Docker containers for tests. More realistic than in-memory: `var container = new PostgreSqlBuilder().Build(); await container.StartAsync();`

**Code Coverage with Coverlet**: Track test coverage with `dotnet add package coverlet.collector` then `dotnet test --collect:"XPlat Code Coverage"`. Aim for 80%+ on critical paths, not 100% everywhere.

**ICollectionFixture for Expensive Resources**: Use `ICollectionFixture<T>` when multiple test classes need the same fixture (like a shared database). Faster than recreating per class.

**WebApplicationFactory for API Testing**: For ASP.NET Core integration tests, use `WebApplicationFactory<Program>` to spin up a test server: `var client = _factory.CreateClient();` - tests real HTTP pipeline.
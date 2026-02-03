---
type: "KEY_POINT"
title: "Migrations and Bulk Operations"
---

## Key Takeaways

- **Migrations version your database schema** -- `dotnet ef migrations add CreateProducts` generates Up/Down methods. `dotnet ef database update` applies them. Every schema change is trackable and reversible.

- **`ExecuteUpdate()` and `ExecuteDelete()` avoid loading entities** -- EF Core 7+ executes bulk operations as single SQL statements. `products.Where(p => p.Discontinued).ExecuteDelete()` is far faster than loading, removing, and saving each entity.

- **Always review generated migration SQL** -- use `dotnet ef migrations script` to see the actual SQL before applying to production. Migrations can drop columns or tables if you are not careful.

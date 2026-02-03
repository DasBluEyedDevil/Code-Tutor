---
type: "KEY_POINT"
title: "EF Core Fundamentals"
---

## Key Takeaways

- **`DbSet<T>` represents a table** -- `DbSet<Product> Products` maps to the Products table. Use LINQ on it to query: `.Where()`, `.Select()`, `.OrderBy()` all translate to SQL.

- **`SaveChanges()` persists all tracked changes** -- add, modify, and remove entities, then call `SaveChanges()` once. EF Core batches the operations into efficient SQL statements within a transaction.

- **Configure providers with `UseSqlite()` or `UseNpgsql()`** -- the provider determines which database engine EF Core talks to. Switch providers by changing one line of configuration.

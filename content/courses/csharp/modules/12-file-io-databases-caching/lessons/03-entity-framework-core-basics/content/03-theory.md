---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`DbContext : DbContext`**: Your DbContext inherits from EF's DbContext. Represents a session with database. Contains DbSet<T> properties for tables.

**`DbSet<T>`**: Represents a table. DbSet<Product> Products = Products table. Use LINQ on DbSet to query. Add/Remove to modify.

**`OnConfiguring()`**: Configure database provider and connection string. UseSqlite(), UseSqlServer(), UseNpgsql() (PostgreSQL). Override in your DbContext.

**`SaveChanges()`**: Persists ALL tracked changes to database! Add, modify, remove objects, then call SaveChanges() ONCE. Batches SQL statements efficiently.
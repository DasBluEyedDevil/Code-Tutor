---
type: "WARNING"
title: "Code-First Design Pitfalls"
---

## Watch Out For These Issues!

**Nullable reference type warnings**: With nullable reference types enabled (default in .NET 9), `string Name { get; set; }` generates warnings. Use `string Name { get; set; } = string.Empty;` for required properties or `string? Name { get; set; }` for truly nullable columns.

**Navigation property null references**: `public Customer Customer { get; set; } = null!;` uses the null-forgiving operator. EF Core populates this via lazy loading or eager loading, but accessing it before loading gives NullReferenceException. Always use `.Include()` to load related data, or check for null.

**Seed data with navigation properties**: `HasData()` in `OnModelCreating()` only accepts anonymous objects with primitive property values and foreign key IDs. You cannot seed data with navigation property objects -- use foreign key values instead: `new { Id = 1, CustomerId = 1 }`.

**Cascade delete surprises**: EF Core enables cascade delete by default for required relationships. Deleting a Customer also deletes all their Orders! If this is not what you want, configure `.OnDelete(DeleteBehavior.Restrict)` in the Fluent API.

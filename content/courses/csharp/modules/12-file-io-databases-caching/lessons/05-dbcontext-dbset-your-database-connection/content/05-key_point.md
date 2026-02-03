---
type: "KEY_POINT"
title: "DbContext Lifecycle and Change Tracking"
---

## Key Takeaways

- **Use `using var context = new AppDbContext();`** -- DbContext implements IDisposable. The `using` declaration ensures the connection is closed and resources released when the scope ends.

- **EF Core tracks entity states automatically** -- Added, Modified, Deleted, Unchanged, Detached. Modify an entity's properties and EF Core knows to generate an UPDATE statement on `SaveChanges()`.

- **Register DbContext as Scoped in ASP.NET Core** -- `AddDbContext<T>()` registers it as Scoped by default: one instance per HTTP request. This prevents concurrency issues between requests.

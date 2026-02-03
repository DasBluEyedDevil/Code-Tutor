---
type: "WARNING"
title: "DbContext and Change Tracking Pitfalls"
---

## Watch Out For These Issues!

**Long-lived DbContext leaks memory**: DbContext caches every entity it tracks. A DbContext that lives too long (Singleton or stored in a static field) accumulates thousands of tracked entities, slowing down SaveChanges and consuming memory. Keep DbContext scoped to a single unit of work.

**Concurrency with shared DbContext**: DbContext is NOT thread-safe! Sharing one instance across multiple threads (e.g., in parallel async calls) causes data corruption and exceptions. Each thread or async operation needs its own DbContext instance. Use `IDbContextFactory<T>` for multi-threaded scenarios.

**Detached entities and updates**: Entities queried from one DbContext instance cannot be saved by a different instance -- they are "detached." You must explicitly attach them: `context.Attach(entity)` then set state to Modified, or re-query and copy values.

**SaveChanges() is transactional**: All changes in a single `SaveChanges()` call succeed or fail together. If you need partial saves (some changes committed even if others fail), call SaveChanges() at multiple points. But be careful -- partial saves can leave data in an inconsistent state.

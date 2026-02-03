---
type: "KEY_POINT"
title: "HybridCache in .NET 9"
---

## Key Takeaways

- **`GetOrCreateAsync(key, factory)` is the core pattern** -- it checks L1 (in-memory), then L2 (distributed like Redis), then runs the factory function. Results are cached at both levels automatically.

- **Tag-based invalidation (.NET 9)** -- `RemoveByTagAsync("products")` clears all cache entries tagged with "products." This solves the common problem of invalidating related cache entries after a data update.

- **Built-in stampede protection** -- when multiple requests hit the same uncached key simultaneously, only one factory call executes. Others wait for its result. This prevents thundering herd problems on cache misses.

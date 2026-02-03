---
type: "WARNING"
title: "HybridCache Pitfalls"
---

## Watch Out For These Issues!

**HybridCache requires .NET 9 or later**: HybridCache (`Microsoft.Extensions.Caching.Hybrid`) is a .NET 9 API. It will NOT compile on .NET 8 or earlier! If your project targets an older framework, you must use the traditional IMemoryCache + IDistributedCache pattern instead. Check your `<TargetFramework>` in .csproj before adding this package.

**Cache invalidation complexity**: "There are only two hard things in computer science: cache invalidation and naming things." When underlying data changes, cached values become stale. Always invalidate cache entries when you modify data. Use tag-based invalidation (`RemoveByTagAsync`) to invalidate groups of related entries.

**Serialization overhead**: HybridCache serializes objects to store in the distributed (L2) cache. Large or complex objects increase serialization/deserialization time. Keep cached objects lean -- cache DTOs, not full entity graphs with navigation properties.

**Cache stampede without HybridCache**: If you ever fall back to manual IDistributedCache, remember it has NO stampede protection. When a popular cache entry expires, hundreds of concurrent requests all hit the database simultaneously. HybridCache handles this automatically, but IDistributedCache does not.

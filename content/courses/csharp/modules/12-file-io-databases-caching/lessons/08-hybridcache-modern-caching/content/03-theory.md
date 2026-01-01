---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`AddHybridCache()`**: Registers HybridCache in DI. Automatically uses IDistributedCache if configured (Redis, SQL Server, CosmosDB, Garnet).

**`GetOrCreateAsync(key, factory)`**: The core method. Checks L1 (memory), then L2 (distributed), then runs factory. Stores result in both caches.

**`HybridCacheEntryOptions`**: Configure per-entry. Expiration (total TTL), LocalCacheExpiration (L1 TTL), Flags (skip local/distributed).

**`tags: ["tag1", "tag2"]`**: Tag entries for bulk invalidation (.NET 9 GA feature). RemoveByTagAsync("tag") removes all entries with that tag.

**Stampede protection**: Built-in! Multiple concurrent requests for same key = only one factory call. Others wait for result. No thundering herd!
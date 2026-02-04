---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Call `.asFlow()` on queries to get reactive Flow<Query<T>>**, then `.mapToList()` or `.mapToOneOrNull()` to observe query results. Flows automatically re-emit when underlying data changes.

**Reactive queries eliminate manual cache invalidation**—UI updates automatically when database writes occur. Insert/update/delete operations trigger recomposition in Compose UIs observing the Flow.

**Flows respect coroutine cancellation and structured concurrency**. When a ViewModel is cleared or a composable leaves composition, Flow collection stops automatically, preventing memory leaks.

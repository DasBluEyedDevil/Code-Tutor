---
type: "KEY_POINT"
title: "Performance Considerations"
---

**Async Django Performance Guide**

**When Async Helps:**

1. **I/O-Bound Operations**
   - External API calls: 10x+ improvement with concurrent requests
   - Database queries: Moderate improvement (depends on backend)
   - File operations: Improvement with multiple files

2. **High Concurrency**
   - Many simultaneous connections
   - WebSockets and long-polling
   - Real-time features

**When Async Doesn't Help:**

1. **CPU-Bound Work**
   - Async runs on one thread
   - CPU work blocks the event loop
   - Use multiprocessing for CPU tasks

2. **Simple CRUD**
   - Single query + response
   - Async overhead exceeds benefit

**Performance Tips:**

1. **Batch sync_to_async calls**
   ```python
   # Bad: Many small wrapped calls
   for item in items:
       await sync_to_async(process)(item)
   
   # Good: One wrapped call for batch
   @sync_to_async
   def process_all(items):
       return [process(item) for item in items]
   ```

2. **Use select_related/prefetch_related**
   ```python
   # Avoid N+1 queries in async loops
   async for tx in Transaction.objects.select_related('user'):
       print(tx.user.username)  # No extra query
   ```

3. **Connection Pooling**
   - Use async-compatible database drivers
   - Configure pool size for your workload
   - PostgreSQL: asyncpg or psycopg3

4. **Don't Mix Blocking Code**
   - Never use `time.sleep()` in async views
   - Use `asyncio.sleep()` instead
   - Wrap all blocking I/O with sync_to_async
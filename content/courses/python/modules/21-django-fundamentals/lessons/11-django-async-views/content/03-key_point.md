---
type: "KEY_POINT"
title: "When to Use Async Views"
---

**Use Async Views When:**

1. **Multiple External API Calls**
   - Fetching from multiple microservices
   - Aggregating data from third-party APIs
   - asyncio.gather() runs them concurrently

2. **WebSockets / Long-Polling**
   - Chat applications
   - Real-time notifications
   - Live updates

3. **Streaming Responses**
   - Large file downloads
   - Server-sent events
   - Progressive rendering

4. **High-Concurrency I/O**
   - Many simultaneous external requests
   - File upload/download handling

**Stick with Sync Views When:**

1. **CPU-Bound Work**
   - Async doesn't help CPU-heavy operations
   - Image processing, calculations run the same speed

2. **Simple CRUD**
   - Single database query + response
   - Async overhead not worth it

3. **Legacy Code Integration**
   - Many sync-only libraries
   - Easier to keep sync views

4. **Team Familiarity**
   - Async has a learning curve
   - Sync code is easier to debug
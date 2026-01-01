---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Connection pooling** is essential - creating connections is expensive (10-50ms each)
- **Pool sizing:** Start with `(CPU cores * 2) + 1`, monitor and adjust
- **min_size** keeps connections ready, **max_size** prevents overload
- **max_inactive_connection_lifetime** cleans up stale connections
- Use **init callback** to configure and verify new connections
- **Batch operations** with executemany are 10-100x faster than individual inserts
- **COPY** is fastest for bulk loading large datasets
- **Cursor streaming** prevents memory exhaustion with large result sets
- **Keyset pagination** is more efficient than OFFSET for deep pages
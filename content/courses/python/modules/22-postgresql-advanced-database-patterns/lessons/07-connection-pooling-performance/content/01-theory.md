---
type: "THEORY"
title: "Why Connection Pooling Matters"
---

Every database connection is expensive - PostgreSQL forks a new process for each connection, consuming memory and CPU. Without pooling:

**The Problem:**
- Connection creation: 10-50ms each
- Memory per connection: ~10MB
- 100 concurrent requests = 100 connections = 1GB RAM just for connections
- Max connections exhausted = new requests fail

**Connection Pooling Solution:**
- Maintain a pool of reusable connections
- Requests borrow from pool, return when done
- Dramatically reduces connection overhead
- Handles connection health and timeouts

**Real-World Impact:**
```
Without pooling:  100 requests/sec x 50ms = 5 seconds of connection time
With pooling:     100 requests/sec x 0.1ms = 10ms of connection time
```

**Finance Tracker Context:**
Our app handles many concurrent users checking balances, adding transactions, and generating reports. Without pooling, we'd quickly exhaust connections during peak usage.
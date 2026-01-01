---
type: "THEORY"
title: "Pool Sizing Strategies"
---

Choosing the right pool size is critical for performance:

**Too Few Connections:**
- Requests wait for available connections
- Increased latency during load
- Underutilized database capacity

**Too Many Connections:**
- Memory exhaustion on database server
- Context switching overhead
- Diminishing returns after optimal point

**Pool Sizing Formula:**
```
Optimal connections = (core_count * 2) + effective_spindle_count
```

For SSD: `(CPU cores * 2) + 1`

**Practical Guidelines:**

| App Type | min_size | max_size |
|----------|----------|----------|
| Small API | 2-5 | 10-20 |
| Medium App | 5-10 | 20-50 |
| High Traffic | 10-20 | 50-100 |

**Key Settings:**
```python
await asyncpg.create_pool(
    dsn,
    min_size=5,           # Always keep this many ready
    max_size=20,          # Never exceed this
    max_inactive_connection_lifetime=300,  # Close idle after 5 min
    command_timeout=60,   # Query timeout
)
```

**Finance Tracker Recommendation:**
- Development: min=2, max=10
- Production: min=5, max=20
- Scale based on monitoring data
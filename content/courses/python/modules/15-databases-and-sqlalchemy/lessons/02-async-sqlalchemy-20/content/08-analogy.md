---
type: "ANALOGY"
title: "Async Database Like Drive-Through Window"
---

**Understanding Async Database Access**

**Sync Database Access (Sit-Down Restaurant):**
You sit at a table, order, and wait. The waiter takes your order to the kitchen, waits for the food, and brings it back. While waiting, you can't do anything else.

```python
# Sync: Server waits for database
def get_users():
    users = session.query(User).all()  # Wait here...
    return users  # ...done waiting
```

**Async Database Access (Drive-Through):**
You order at the speaker, pull to the window, and your order is prepared while others also order. Multiple orders processed simultaneously.

```python
# Async: Server handles other requests while waiting
async def get_users():
    users = await async_session.scalars(select(User))  # Don't wait!
    return users.all()  # Other requests processed meanwhile
```

**The Key Difference:**

| Sync (Sit-Down) | Async (Drive-Through) |
|-----------------|----------------------|
| 1 customer at a time | Multiple customers simultaneously |
| Waiter waits with you | Waiter serves multiple customers |
| Simple but slow | Complex but fast |
| Good for low traffic | Good for high traffic |

**In Code Terms:**

```python
# Sync: Sequential, one at a time
def process_orders():
    order1 = get_order(1)  # Wait 100ms
    order2 = get_order(2)  # Wait 100ms
    order3 = get_order(3)  # Wait 100ms
    # Total: 300ms

# Async: Concurrent, overlapping waits
async def process_orders():
    order1, order2, order3 = await asyncio.gather(
        get_order(1),  # All start
        get_order(2),  # at the
        get_order(3),  # same time
    )
    # Total: ~100ms (overlapped)
```

**When to Go Async:**
- High-concurrency APIs (FastAPI)
- Many database operations per request
- I/O-bound workloads (web apps)

**When Sync is Fine:**
- Scripts and CLI tools
- Low-traffic applications
- Simpler code is priority

---
type: "KEY_POINT"
title: "One Event Loop Per Thread"
---

**Critical concept:** Each thread has exactly one event loop.

**Common patterns:**

```python
# Pattern 1: asyncio.run() - creates new loop, runs, closes
asyncio.run(main())  # Most common

# Pattern 2: asyncio.gather() - run multiple coroutines
await asyncio.gather(coro1, coro2, coro3)

# Pattern 3: asyncio.create_task() - schedule for later
task = asyncio.create_task(my_coro())
# ... do other things ...
result = await task
```

**gather() vs create_task():**
- `gather()`: Wait for ALL results together
- `create_task()`: Fire-and-forget, collect results later

**When to use which:**
- Need all results at once -> `gather()`
- Need to do work while tasks run -> `create_task()`
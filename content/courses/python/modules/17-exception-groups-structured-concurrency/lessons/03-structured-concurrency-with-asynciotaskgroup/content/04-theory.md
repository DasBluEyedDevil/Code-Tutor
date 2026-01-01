---
type: "THEORY"
title: "TaskGroup vs gather"
---

| Feature | asyncio.gather | asyncio.TaskGroup |
|---------|----------------|-------------------|
| Error handling | First error raised | All errors in ExceptionGroup |
| Cancellation | Manual | Automatic on failure |
| Task cleanup | Background tasks may linger | All tasks guaranteed done |
| Syntax | `await gather(...)` | `async with TaskGroup()` |
| Python version | 3.4+ | 3.11+ |

**When to use TaskGroup:**
- Multiple concurrent operations that should succeed or fail together
- When you need all errors, not just the first
- When clean cancellation is important

**When to use gather:**
- Legacy code (pre-3.11)
- When you need `return_exceptions=True` behavior
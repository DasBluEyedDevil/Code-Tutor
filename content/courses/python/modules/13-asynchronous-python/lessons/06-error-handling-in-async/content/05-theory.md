---
type: "THEORY"
title: "asyncio.timeout() - Modern Timeout Handling (Python 3.11+)"
---

**Python 3.11 introduced asyncio.timeout()**

This is a cleaner alternative to `asyncio.wait_for()` using context managers:

**Old way (wait_for):**
```python
try:
    result = await asyncio.wait_for(slow_task(), timeout=5.0)
except asyncio.TimeoutError:
    print("Timed out!")
```

**New way (asyncio.timeout):**
```python
try:
    async with asyncio.timeout(5.0):
        result = await slow_task()
except TimeoutError:  # Note: TimeoutError, not asyncio.TimeoutError
    print("Timed out!")
```

**Benefits of asyncio.timeout():**
- Context manager syntax is cleaner
- Can wrap multiple operations
- Better cancellation handling
- Catches `TimeoutError` (not `asyncio.TimeoutError`)

**asyncio.timeout_at() for absolute deadlines:**
```python
# Deadline is 30 seconds from now
deadline = asyncio.get_running_loop().time() + 30.0
async with asyncio.timeout_at(deadline):
    await task1()
    await task2()  # Same deadline applies to both
```
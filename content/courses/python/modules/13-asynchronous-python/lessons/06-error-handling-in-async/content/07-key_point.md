---
type: "KEY_POINT"
title: "timeout() vs wait_for(): When to Use Which"
---

**asyncio.timeout() (Python 3.11+)**

Use when:
- You want cleaner context manager syntax
- You need to timeout multiple sequential operations
- You want to combine with TaskGroup
- You need absolute deadlines (timeout_at)

```python
async with asyncio.timeout(10.0):
    data1 = await fetch_one()
    data2 = await fetch_two()  # Same timeout applies
```

**asyncio.wait_for() (Any Python 3.x)**

Use when:
- You need to support Python < 3.11
- You only need to timeout a single operation
- You want the timeout value inline with the call

```python
result = await asyncio.wait_for(fetch_one(), timeout=10.0)
```

**Key Differences:**

| Feature | timeout() | wait_for() |
|---------|-----------|------------|
| Python version | 3.11+ | Any |
| Syntax | Context manager | Function |
| Multiple ops | Yes | No (one per call) |
| Exception | TimeoutError | asyncio.TimeoutError |
| Absolute deadline | timeout_at() | No |
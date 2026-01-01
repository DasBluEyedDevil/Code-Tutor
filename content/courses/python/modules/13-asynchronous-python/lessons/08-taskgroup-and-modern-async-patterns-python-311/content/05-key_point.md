---
type: "KEY_POINT"
title: "TaskGroup Best Practices"
---

**1. Name your tasks for better debugging:**
```python
tg.create_task(fetch_data(), name="fetch_user_data")
```

**2. Use except* to handle specific exception types:**
```python
try:
    async with asyncio.TaskGroup() as tg:
        ...
except* HTTPError as eg:
    # Handle HTTP errors
except* TimeoutError as eg:
    # Handle timeouts
```

**3. Combine with timeout for robustness:**
```python
async with asyncio.timeout(30.0):
    async with asyncio.TaskGroup() as tg:
        tg.create_task(fetch_accounts())
        tg.create_task(fetch_transactions())
```

**4. TaskGroup for atomic operations:**
When you need "all or nothing" semantics, TaskGroup ensures either all tasks complete or all are cancelled.

**5. Python version check:**
```python
import sys
if sys.version_info >= (3, 11):
    # Use TaskGroup
else:
    # Fall back to gather()
```
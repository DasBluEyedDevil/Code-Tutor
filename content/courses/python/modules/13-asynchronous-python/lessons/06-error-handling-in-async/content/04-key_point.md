---
type: "KEY_POINT"
title: "gather() with return_exceptions=True"
---

**When to use `return_exceptions=True`:**

1. **When you want ALL tasks to complete** (even if some fail)
2. **When you need to handle each error individually**
3. **When failures are expected** (like network requests)

**Pattern for handling mixed results:**
```python
results = await asyncio.gather(*tasks, return_exceptions=True)

successes = []
errors = []

for result in results:
    if isinstance(result, Exception):
        errors.append(result)
    else:
        successes.append(result)

print(f"{len(successes)} succeeded, {len(errors)} failed")
```

**Best practice:** Wrap individual tasks in try/except for graceful handling.
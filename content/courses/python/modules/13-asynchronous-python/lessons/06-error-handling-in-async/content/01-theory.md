---
type: "THEORY"
title: "How Exceptions Propagate in Async Code"
---

**Exceptions in async code work similarly to sync code:**

```python
async def might_fail():
    raise ValueError("Oops!")

try:
    await might_fail()
except ValueError as e:
    print(f"Caught: {e}")
```

**But with concurrent tasks, it's trickier:**

With `asyncio.gather()`, if one task fails:
- By default, ALL tasks are cancelled
- First exception is raised

**Solution: `return_exceptions=True`**
```python
results = await asyncio.gather(
    task1(),
    task2(),  # This might fail
    task3(),
    return_exceptions=True
)
# results = [result1, ValueError(...), result3]
```

Now exceptions are returned as values instead of raised!
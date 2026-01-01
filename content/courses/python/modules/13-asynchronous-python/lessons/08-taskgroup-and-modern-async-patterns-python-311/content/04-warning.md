---
type: "WARNING"
title: "TaskGroup vs gather(): When to Use Which"
---

### TaskGroup (Python 3.11+)
**Use when:**
- You want automatic cancellation on any failure
- You need all tasks to succeed or none
- You want to catch multiple exceptions cleanly
- You prefer structured concurrency

```python
async with asyncio.TaskGroup() as tg:
    tg.create_task(must_succeed_1())
    tg.create_task(must_succeed_2())
# If any fails, ALL are cancelled
```

### gather() with return_exceptions=True
**Use when:**
- You want partial results even if some fail
- You need to process results individually
- You're on Python < 3.11

```python
results = await asyncio.gather(
    might_fail_1(),
    might_fail_2(),
    return_exceptions=True
)
# Get all results, check each for Exception
```

### Common Mistakes

**1. Using TaskGroup when partial results are OK**
```python
# WRONG - If any fails, lose ALL results
async with asyncio.TaskGroup() as tg:
    for url in urls:
        tg.create_task(fetch(url))

# BETTER - Get partial results
results = await asyncio.gather(
    *[fetch(url) for url in urls],
    return_exceptions=True
)
successful = [r for r in results if not isinstance(r, Exception)]
```

**2. Forgetting except* for ExceptionGroup**
```python
# WRONG - Regular except won't catch ExceptionGroup properly
try:
    async with asyncio.TaskGroup() as tg:
        ...
except ConnectionError:  # Won't work!
    pass

# CORRECT - Use except* for ExceptionGroup
except* ConnectionError as eg:
    for exc in eg.exceptions:
        handle(exc)
```
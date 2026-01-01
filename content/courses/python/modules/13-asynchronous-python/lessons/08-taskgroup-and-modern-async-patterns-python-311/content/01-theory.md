---
type: "THEORY"
title: "Structured Concurrency with TaskGroup"
---

**Python 3.11+ introduces asyncio.TaskGroup**

TaskGroup provides **structured concurrency** - a cleaner way to manage multiple async tasks that ensures proper cleanup and error handling.

**The Problem with gather():**
```python
# With gather, error handling is awkward
results = await asyncio.gather(
    task1(),
    task2(),  # If this fails...
    task3(),
    return_exceptions=True  # ...you get mixed results/exceptions
)
# Have to check each result manually
```

**The TaskGroup Solution (Python 3.11+):**
```python
async with asyncio.TaskGroup() as tg:
    task1 = tg.create_task(fetch_accounts())
    task2 = tg.create_task(fetch_transactions())
    task3 = tg.create_task(fetch_budgets())

# All tasks complete before exiting the block
# If ANY task fails, ALL others are cancelled
# ExceptionGroup is raised with all errors
```

**Benefits of TaskGroup:**
- **Automatic cleanup**: All tasks cancelled on error
- **All errors captured**: ExceptionGroup contains every exception
- **Cleaner syntax**: No need for return_exceptions
- **Named tasks**: Better debugging with task names
- **Guaranteed completion**: Block doesn't exit until all tasks done
---
type: "THEORY"
title: "What is Structured Concurrency?"
---

**Structured concurrency** is a design pattern where concurrent tasks have clear lifetimes tied to a scope.

**The old way (asyncio.gather):**
```python
results = await asyncio.gather(task1(), task2(), task3())
# Problem: If task2 fails, what happens to task1 and task3?
# Answer: They might keep running in the background!
```

**The new way (asyncio.TaskGroup - Python 3.11+):**
```python
async with asyncio.TaskGroup() as tg:
    tg.create_task(task1())
    tg.create_task(task2())
    tg.create_task(task3())
# When the block exits, ALL tasks are guaranteed to be done
# If any fail, all others are cancelled and errors are grouped
```

**Benefits:**
- No orphaned background tasks
- All errors collected in an ExceptionGroup
- Clean cancellation on failure

**Finance Tracker Use Case:**
Imagine loading a user's financial dashboard. You need to fetch accounts, transactions, and budgets simultaneously. If any fetch fails, you want to know ALL failures, not just the first one - and you don't want orphaned API calls running in the background.
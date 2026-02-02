---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using Regular try/except Instead of except***
```python
# WRONG - Regular except catches ExceptionGroup, not individual errors
async with asyncio.TaskGroup() as tg:
    tg.create_task(risky_task())
except ConnectionError:  # Won't catch ConnectionError inside group!
    pass

# CORRECT - Use except* for TaskGroup errors
try:
    async with asyncio.TaskGroup() as tg:
        tg.create_task(risky_task())
except* ConnectionError as eg:  # Catches ConnectionErrors from tasks
    for exc in eg.exceptions:
        print(exc)
```

**2. Accessing Task Results Before Context Exit**
```python
# WRONG - Tasks not complete yet
async with asyncio.TaskGroup() as tg:
    task = tg.create_task(fetch_data())
    result = task.result()  # InvalidStateError - not done yet!

# CORRECT - Access results after context block
async with asyncio.TaskGroup() as tg:
    task = tg.create_task(fetch_data())
# Now task is complete
result = task.result()
```

**3. Forgetting async with (Using Regular with)**
```python
# WRONG - TaskGroup needs async with
with asyncio.TaskGroup() as tg:  # TypeError!
    tg.create_task(my_coro())

# CORRECT - Use async with
async with asyncio.TaskGroup() as tg:
    tg.create_task(my_coro())
```

**4. Creating Tasks Outside the Context Manager**
```python
# WRONG - Cannot add tasks after context starts
async with asyncio.TaskGroup() as tg:
    pass
tg.create_task(fetch())  # RuntimeError: TaskGroup closed!

# CORRECT - Create tasks inside the context
async with asyncio.TaskGroup() as tg:
    tg.create_task(fetch())  # Create while context is open
```

**5. Not Handling CancelledError in Tasks**
```python
# WRONG - Task ignores cancellation, causes issues
async def bad_task():
    try:
        await long_operation()
    except asyncio.CancelledError:
        pass  # Swallowing cancellation!

# CORRECT - Re-raise CancelledError or clean up properly
async def good_task():
    try:
        await long_operation()
    except asyncio.CancelledError:
        await cleanup()
        raise  # Re-raise after cleanup
```
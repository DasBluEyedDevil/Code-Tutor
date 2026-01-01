---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Awaiting Non-Awaitable Objects**
```python
# WRONG - Regular function not awaitable
def regular_func():
    return 'data'

async def main():
    result = await regular_func()  # TypeError!

# CORRECT - Only await async functions
async def async_func():
    return 'data'

async def main():
    result = await async_func()  # Works!
```

**2. Using asyncio.run() Inside Async Context**
```python
# WRONG - Can't nest asyncio.run()
async def main():
    result = asyncio.run(other_async())  # RuntimeError!

# CORRECT - Just await it
async def main():
    result = await other_async()  # Works!
```

**3. Awaiting Inside List Comprehension**
```python
# WRONG - Can't use await in list comprehension directly
async def main():
    results = [await fetch(i) for i in range(3)]  # Runs sequentially!

# CORRECT - Use asyncio.gather for concurrency
async def main():
    results = await asyncio.gather(*[fetch(i) for i in range(3)])
```

**4. Calling Coroutine Without Scheduling**
```python
# WRONG - Coroutine created but never runs
async def main():
    fetch_data()  # Warning: coroutine never awaited!

# CORRECT - Use await or schedule it
async def main():
    await fetch_data()  # Runs and waits
    # Or create a task
    task = asyncio.create_task(fetch_data())
```

**5. Infinite Loop Without Yielding**
```python
# WRONG - Blocks event loop forever
async def bad_loop():
    while True:
        process_data()  # No await, blocks!

# CORRECT - Yield control periodically
async def good_loop():
    while True:
        process_data()
        await asyncio.sleep(0)  # Yield to event loop
```
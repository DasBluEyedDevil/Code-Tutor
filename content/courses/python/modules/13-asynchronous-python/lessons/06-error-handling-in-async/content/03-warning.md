---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. One Failed Task Cancels All Others**
```python
# WRONG - One error cancels everything
results = await asyncio.gather(
    fetch(url1),
    fetch(url2),  # If this fails, url1 and url3 cancelled!
    fetch(url3)
)

# CORRECT - Use return_exceptions=True
results = await asyncio.gather(
    fetch(url1),
    fetch(url2),
    fetch(url3),
    return_exceptions=True  # All complete!
)
```

**2. Not Checking for Exceptions in Results**
```python
# WRONG - Results may contain exceptions
results = await asyncio.gather(*tasks, return_exceptions=True)
for r in results:
    print(r['data'])  # May be Exception, not dict!

# CORRECT - Check each result
for r in results:
    if isinstance(r, Exception):
        print(f'Error: {r}')
    else:
        print(r['data'])
```

**3. Forgetting TimeoutError Handling**
```python
# WRONG - TimeoutError not caught
result = await asyncio.wait_for(slow_task(), timeout=5)
# If timeout, crashes!

# CORRECT - Handle timeout
try:
    result = await asyncio.wait_for(slow_task(), timeout=5)
except asyncio.TimeoutError:
    result = None  # Handle timeout gracefully
```

**4. Swallowing Exceptions Silently**
```python
# WRONG - Exception ignored
async def bad_fetch(url):
    try:
        return await client.get(url)
    except:
        pass  # Silent failure, returns None!

# CORRECT - Log or re-raise
async def good_fetch(url):
    try:
        return await client.get(url)
    except Exception as e:
        logger.error(f'Fetch failed: {e}')
        raise  # Or return error indicator
```

**5. CancelledError Not Re-raised**
```python
# WRONG - Swallowing CancelledError breaks cancellation
async def bad():
    try:
        await some_task()
    except:
        print('Error')  # Catches CancelledError too!

# CORRECT - Re-raise CancelledError
async def good():
    try:
        await some_task()
    except asyncio.CancelledError:
        raise  # Must re-raise!
    except Exception as e:
        print(f'Error: {e}')
```
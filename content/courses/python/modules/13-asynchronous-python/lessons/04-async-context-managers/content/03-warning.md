---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using Regular with Instead of async with**
```python
# WRONG - with doesn't call __aenter__/__aexit__
async def fetch():
    with httpx.AsyncClient() as client:  # Error or wrong behavior
        return await client.get(url)

# CORRECT - Use async with
async def fetch():
    async with httpx.AsyncClient() as client:
        return await client.get(url)
```

**2. Creating New Client for Each Request**
```python
# WRONG - Inefficient, creates new connection each time
async def fetch_many(urls):
    for url in urls:
        async with httpx.AsyncClient() as client:
            await client.get(url)  # New connection each time!

# CORRECT - Reuse client for multiple requests
async def fetch_many(urls):
    async with httpx.AsyncClient() as client:
        for url in urls:
            await client.get(url)  # Reuses connection
```

**3. Forgetting await in Async Context Manager**
```python
# WRONG - Returns coroutine not result
async def read_file():
    async with aiofiles.open('file.txt') as f:
        return f.read()  # Returns coroutine!

# CORRECT - Await the read
async def read_file():
    async with aiofiles.open('file.txt') as f:
        return await f.read()  # Returns content
```

**4. Not Handling Exceptions in Context Manager**
```python
# WRONG - Exception leaves resource unclosed
async def bad():
    client = httpx.AsyncClient()
    await client.get(url)  # If error, never closed!

# CORRECT - Use async with for cleanup
async def good():
    async with httpx.AsyncClient() as client:
        await client.get(url)  # Closed even on error
```

**5. Mixing Sync and Async Context Managers**
```python
# WRONG - Can't use sync context manager in async
async def bad():
    with open('file.txt') as f:  # Blocks!
        return f.read()

# CORRECT - Use async file library
import aiofiles
async def good():
    async with aiofiles.open('file.txt') as f:
        return await f.read()  # Non-blocking
```
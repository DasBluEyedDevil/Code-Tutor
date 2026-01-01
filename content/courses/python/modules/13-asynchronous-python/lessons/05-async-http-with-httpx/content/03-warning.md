---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using requests Instead of httpx in Async Code**
```python
# WRONG - requests blocks the event loop
async def fetch():
    import requests
    return requests.get(url)  # Blocks everything!

# CORRECT - Use httpx for async
async def fetch():
    async with httpx.AsyncClient() as client:
        return await client.get(url)  # Non-blocking
```

**2. Not Handling HTTP Errors**
```python
# WRONG - No error handling
async def fetch():
    async with httpx.AsyncClient() as client:
        return await client.get(url)  # May fail silently

# CORRECT - Check status and handle errors
async def fetch():
    async with httpx.AsyncClient() as client:
        response = await client.get(url)
        response.raise_for_status()  # Raises on 4xx/5xx
        return response.json()
```

**3. Not Setting Timeouts**
```python
# WRONG - No timeout, can hang forever
async with httpx.AsyncClient() as client:
    await client.get(url)  # May wait forever

# CORRECT - Always set timeout
async with httpx.AsyncClient(timeout=30.0) as client:
    await client.get(url)  # Fails after 30s
```

**4. Making Too Many Concurrent Requests**
```python
# WRONG - May overwhelm server or get rate limited
urls = [url for url in range(1000)]  # 1000 URLs!
await asyncio.gather(*[client.get(u) for u in urls])  # Boom!

# CORRECT - Limit concurrency with semaphore
sem = asyncio.Semaphore(10)  # Max 10 at a time
async def limited_fetch(url):
    async with sem:
        return await client.get(url)
```

**5. Forgetting to Parse Response**
```python
# WRONG - response is Response object, not data
async def get_users():
    async with httpx.AsyncClient() as client:
        return await client.get(url)  # Returns Response!

users = await get_users()
print(users['name'])  # TypeError!

# CORRECT - Parse JSON response
async def get_users():
    async with httpx.AsyncClient() as client:
        response = await client.get(url)
        return response.json()  # Returns dict!
```
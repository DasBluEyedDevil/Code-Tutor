---
type: "KEY_POINT"
title: "Connection Pooling with AsyncClient"
---

**Always use `async with httpx.AsyncClient()`:**

```python
# GOOD - connections are pooled and reused
async with httpx.AsyncClient() as client:
    for url in urls:
        response = await client.get(url)

# BAD - creates new connection for each request
for url in urls:
    async with httpx.AsyncClient() as client:
        response = await client.get(url)
```

**Benefits of connection pooling:**
- Reuses TCP connections (faster)
- Reduces server load
- Automatic connection management
- Proper cleanup on exit

**Timeouts and limits:**
```python
async with httpx.AsyncClient(
    timeout=10.0,
    limits=httpx.Limits(max_connections=100)
) as client:
    ...
```
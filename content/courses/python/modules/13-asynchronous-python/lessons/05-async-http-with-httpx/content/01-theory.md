---
type: "THEORY"
title: "httpx vs requests Library"
---

**The Problem with requests:**

The popular `requests` library is synchronous. Each request blocks until complete.

```python
import requests

# This blocks for each request
r1 = requests.get(url1)  # Wait 1s
r2 = requests.get(url2)  # Wait 1s
r3 = requests.get(url3)  # Wait 1s
# Total: 3 seconds
```

**httpx: The async alternative**

```python
import httpx

async with httpx.AsyncClient() as client:
    # All requests run concurrently
    r1, r2, r3 = await asyncio.gather(
        client.get(url1),
        client.get(url2),
        client.get(url3)
    )
# Total: ~1 second
```

**Why httpx?**
- Supports both sync and async
- HTTP/2 support
- Modern Python API
- Connection pooling built-in
- Similar API to requests (easy migration)
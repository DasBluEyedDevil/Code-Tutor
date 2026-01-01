---
type: "THEORY"
title: "Practical Async Patterns"
---

**Building a real async application:**

When scraping websites or making many API calls, you need:

1. **Rate limiting** - Don't overwhelm servers
2. **Error handling** - Network fails, sites go down
3. **Concurrency control** - Limit parallel requests
4. **Progress tracking** - Know what's happening

**The Semaphore pattern:**
```python
semaphore = asyncio.Semaphore(10)  # Max 10 concurrent

async def limited_fetch(url):
    async with semaphore:
        return await fetch(url)
```

**Rate limiting with delays:**
```python
async def polite_fetch(url):
    result = await fetch(url)
    await asyncio.sleep(0.5)  # Be nice to servers!
    return result
```
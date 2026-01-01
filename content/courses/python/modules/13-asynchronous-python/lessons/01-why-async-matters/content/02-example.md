---
type: "EXAMPLE"
title: "Synchronous vs Asynchronous Comparison"
---

**Sync approach:**
```python
import time

def fetch_data(url):
    time.sleep(1)  # Simulate network delay
    return f"Data from {url}"

# Takes 3 seconds total (1 + 1 + 1)
result1 = fetch_data("url1")
result2 = fetch_data("url2")
result3 = fetch_data("url3")
```

**Async approach:**
```python
import asyncio

async def fetch_data(url):
    await asyncio.sleep(1)  # Non-blocking wait
    return f"Data from {url}"

# Takes ~1 second total (all run concurrently)
results = await asyncio.gather(
    fetch_data("url1"),
    fetch_data("url2"),
    fetch_data("url3")
)
```

**Key differences:**
- `async def` creates a coroutine
- `await` pauses execution without blocking
- `asyncio.gather()` runs multiple coroutines concurrently

```python
import time

# Simulating synchronous approach
print("=== Synchronous (Blocking) ===")

def sync_fetch(url):
    """Simulates a blocking network request"""
    print(f"  Fetching {url}...")
    time.sleep(0.5)  # Blocking wait
    return f"Data from {url}"

start = time.time()
results = []
for url in ["api/users", "api/posts", "api/comments"]:
    results.append(sync_fetch(url))
end = time.time()

print(f"  Results: {results}")
print(f"  Total time: {end - start:.2f}s (sequential)\n")

# Simulating async approach (conceptual)
print("=== Asynchronous (Non-blocking) Concept ===")
print("  With async, all 3 requests run concurrently")
print("  Total time would be ~0.5s instead of 1.5s")
print("  3x faster for I/O-bound operations!\n")

# Show the pattern
print("=== The Async Pattern ===")
async_code = '''
import asyncio

async def fetch(url):
    await asyncio.sleep(0.5)  # Non-blocking
    return f"Data from {url}"

async def main():
    # Run all 3 concurrently
    results = await asyncio.gather(
        fetch("api/users"),
        fetch("api/posts"),
        fetch("api/comments")
    )
    return results

# Run the async code
results = asyncio.run(main())
'''
print(async_code)
```

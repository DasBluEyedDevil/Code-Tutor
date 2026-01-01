---
type: "EXAMPLE"
title: "Running Coroutines Concurrently"
---

**Sequential vs Concurrent execution:**

**Sequential (slow):**
```python
result1 = await fetch(url1)  # Wait 1s
result2 = await fetch(url2)  # Wait 1s
result3 = await fetch(url3)  # Wait 1s
# Total: 3 seconds
```

**Concurrent with gather (fast):**
```python
results = await asyncio.gather(
    fetch(url1),
    fetch(url2),
    fetch(url3)
)
# Total: ~1 second (all run at once)
```

```python
import asyncio
import time

async def fetch_data(name, delay):
    """Simulates an async operation with variable delay"""
    print(f"  [{name}] Starting...")
    await asyncio.sleep(delay)
    print(f"  [{name}] Done after {delay}s!")
    return f"{name}_result"

async def main():
    print("=== Sequential Execution ===")
    start = time.time()
    
    # Each await blocks until complete
    r1 = await fetch_data("Task1", 0.5)
    r2 = await fetch_data("Task2", 0.5)
    r3 = await fetch_data("Task3", 0.5)
    
    print(f"  Results: {[r1, r2, r3]}")
    print(f"  Time: {time.time() - start:.2f}s\n")
    
    print("=== Concurrent with asyncio.gather() ===")
    start = time.time()
    
    # All tasks run concurrently!
    results = await asyncio.gather(
        fetch_data("TaskA", 0.5),
        fetch_data("TaskB", 0.5),
        fetch_data("TaskC", 0.5)
    )
    
    print(f"  Results: {results}")
    print(f"  Time: {time.time() - start:.2f}s (3x faster!)\n")
    
    print("=== Using asyncio.create_task() ===")
    start = time.time()
    
    # Create tasks (they start running immediately)
    task1 = asyncio.create_task(fetch_data("TaskX", 0.3))
    task2 = asyncio.create_task(fetch_data("TaskY", 0.5))
    task3 = asyncio.create_task(fetch_data("TaskZ", 0.4))
    
    # Do other work while tasks run...
    print("  [Main] Tasks started, doing other work...")
    await asyncio.sleep(0.1)
    print("  [Main] Other work done, waiting for tasks...")
    
    # Collect results
    results = [await task1, await task2, await task3]
    print(f"  Results: {results}")
    print(f"  Time: {time.time() - start:.2f}s")

print("Event Loop Demo\n")
asyncio.run(main())
```

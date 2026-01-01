---
type: "EXAMPLE"
title: "Handling Errors with TaskGroup and wait_for"
---

**Python 3.11+ TaskGroup:**
```python
async with asyncio.TaskGroup() as tg:
    tg.create_task(task1())
    tg.create_task(task2())
# All tasks complete or all are cancelled on error
```

**Timeouts with wait_for:**
```python
try:
    result = await asyncio.wait_for(slow_task(), timeout=5.0)
except asyncio.TimeoutError:
    print("Task took too long!")
```

```python
import asyncio

async def successful_task(name):
    await asyncio.sleep(0.2)
    return f"{name} completed"

async def failing_task(name):
    await asyncio.sleep(0.1)
    raise ValueError(f"{name} failed!")

async def slow_task():
    await asyncio.sleep(5)  # Very slow
    return "Finally done"

async def main():
    print("=== gather() Default Behavior ===")
    try:
        results = await asyncio.gather(
            successful_task("Task1"),
            failing_task("Task2"),
            successful_task("Task3")
        )
    except ValueError as e:
        print(f"  Caught exception: {e}")
        print("  (Other tasks were cancelled)\n")
    
    print("=== gather() with return_exceptions=True ===")
    results = await asyncio.gather(
        successful_task("TaskA"),
        failing_task("TaskB"),
        successful_task("TaskC"),
        return_exceptions=True
    )
    
    for i, result in enumerate(results):
        if isinstance(result, Exception):
            print(f"  Task {i}: ERROR - {result}")
        else:
            print(f"  Task {i}: {result}")
    
    print("\n=== Timeout with wait_for() ===")
    try:
        # This will timeout after 0.5 seconds
        result = await asyncio.wait_for(
            slow_task(),
            timeout=0.5
        )
    except asyncio.TimeoutError:
        print("  Task timed out after 0.5s!")
    
    print("\n=== Individual Error Handling ===")
    
    async def safe_fetch(url):
        """Wrapper that handles errors gracefully"""
        try:
            await asyncio.sleep(0.1)
            if "bad" in url:
                raise ConnectionError(f"Failed to connect to {url}")
            return {"url": url, "data": "success"}
        except ConnectionError as e:
            return {"url": url, "error": str(e)}
    
    urls = ["good.com", "bad.com", "also-good.com"]
    results = await asyncio.gather(*[safe_fetch(url) for url in urls])
    
    for result in results:
        if "error" in result:
            print(f"  FAIL: {result['url']} - {result['error']}")
        else:
            print(f"  OK: {result['url']}")

asyncio.run(main())
```

---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting await**
```python
# WRONG - Returns coroutine object, not content!
async with aiofiles.open(path) as f:
    content = f.read()  # Missing await!
    print(content)  # <coroutine object ...>

# CORRECT
async with aiofiles.open(path) as f:
    content = await f.read()  # Add await
    print(content)  # Actual file content
```

**2. Using aiofiles Outside Async Context**
```python
# WRONG - Can't use async with in regular function
def read_file(path):
    async with aiofiles.open(path) as f:  # SyntaxError!
        return await f.read()

# CORRECT - Use async function
async def read_file(path):
    async with aiofiles.open(path) as f:
        return await f.read()

# Or run in event loop
result = asyncio.run(read_file('data.txt'))
```

**3. Not Using asyncio.gather for Concurrency**
```python
# WRONG - Sequential, defeats the purpose
async def process_files(paths):
    results = []
    for path in paths:
        content = await read_file(path)  # Waits each time
        results.append(content)
    return results

# CORRECT - Concurrent execution
async def process_files(paths):
    tasks = [read_file(path) for path in paths]
    return await asyncio.gather(*tasks)  # All at once!
```

**4. Mixing Sync and Async File Operations**
```python
# WRONG - Blocks the event loop
async def bad_example():
    with open('file.txt') as f:  # Blocking!
        return f.read()

# CORRECT - Use aiofiles consistently
async def good_example():
    async with aiofiles.open('file.txt') as f:
        return await f.read()
```
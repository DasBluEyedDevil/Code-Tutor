---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **aiofiles provides async file I/O** for non-blocking file operations
- **Install with:** `pip install aiofiles`
- **Use `async with`** instead of regular `with` for file context managers
- **Always `await`** file operations: `await f.read()`, `await f.write()`
- **Use `asyncio.gather()`** to read/write multiple files concurrently
- **Line iteration:** `async for line in f:` works like regular iteration
- **Best for:** Processing many files, async web apps, concurrent I/O
- **Not needed for:** Single files, simple scripts, CPU-bound tasks
- **Error handling:** Same exceptions as regular file I/O (FileNotFoundError, etc.)

### Quick Reference:
```python
import aiofiles
import asyncio

# Read file
async def read(path):
    async with aiofiles.open(path, 'r') as f:
        return await f.read()

# Write file
async def write(path, content):
    async with aiofiles.open(path, 'w') as f:
        await f.write(content)

# Process many files concurrently
async def process_all(paths):
    return await asyncio.gather(*[read(p) for p in paths])

# Run from sync code
result = asyncio.run(read('file.txt'))
```
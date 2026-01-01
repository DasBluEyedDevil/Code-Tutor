---
type: "THEORY"
title: "Understanding Async File I/O"
---

**The Problem with Synchronous File I/O:**

When you read or write a file with regular `open()`, your program **blocks** - it stops and waits for the disk operation to complete. For a single file, this is fine. But what if you need to:

- Process 100 transaction files simultaneously?
- Read files while handling web requests?
- Update multiple finance reports at once?

**Synchronous (blocking):**
```python
# Each file blocks until complete
for filename in files:
    content = open(filename).read()  # Waits here...
    process(content)
```

**Asynchronous (non-blocking):**
```python
# Start all reads, process as they complete
import aiofiles
import asyncio

async def read_file(path):
    async with aiofiles.open(path) as f:
        return await f.read()

# All files read concurrently!
contents = await asyncio.gather(*[read_file(f) for f in files])
```

### What is aiofiles?

**aiofiles** is a library that provides async versions of Python's file operations:

```python
# Install with: pip install aiofiles
import aiofiles

async def read_file_async(path: str) -> str:
    async with aiofiles.open(path, 'r') as f:
        return await f.read()
```

**Key differences from regular file I/O:**
- Uses `async with` instead of `with`
- Uses `await` for all file operations
- Allows other code to run while waiting for disk I/O

### When to Use aiofiles:

**Good use cases:**
- Processing many files concurrently
- Building async web applications (FastAPI, aiohttp)
- Background file processing tasks
- Real-time data ingestion pipelines

**Not needed when:**
- Reading a single file
- Simple scripts without async code
- CPU-bound processing (async won't help)
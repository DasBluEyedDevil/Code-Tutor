---
type: "EXAMPLE"
title: "Using async with for File I/O"
---

**aiofiles library:**
```python
import aiofiles

async with aiofiles.open('data.txt', 'r') as f:
    content = await f.read()

async with aiofiles.open('output.txt', 'w') as f:
    await f.write('Hello, World!')
```

**Why use aiofiles instead of regular open()?**
- Regular file I/O blocks the event loop
- aiofiles uses thread pool for non-blocking file access
- Allows other coroutines to run during file operations

```python
import asyncio

# Note: In real code, you'd use aiofiles library
# pip install aiofiles

# Simulating async file operations
class AsyncFile:
    """Simulates aiofiles behavior for demonstration"""
    
    def __init__(self, filename, mode='r'):
        self.filename = filename
        self.mode = mode
        self.content = ""
    
    async def __aenter__(self):
        """Async context manager entry"""
        print(f"  Opening {self.filename}...")
        await asyncio.sleep(0.1)  # Simulate async open
        return self
    
    async def __aexit__(self, exc_type, exc_val, exc_tb):
        """Async context manager exit - cleanup"""
        print(f"  Closing {self.filename}...")
        await asyncio.sleep(0.05)  # Simulate async close
        return False
    
    async def read(self):
        await asyncio.sleep(0.1)  # Simulate async read
        return f"Content of {self.filename}"
    
    async def write(self, data):
        await asyncio.sleep(0.1)  # Simulate async write
        self.content = data
        print(f"  Wrote: {data[:50]}...")

async def demo_async_file():
    print("=== Async File Reading ===")
    
    async with AsyncFile('data.txt', 'r') as f:
        content = await f.read()
        print(f"  Read: {content}")
    
    print("\n=== Async File Writing ===")
    
    async with AsyncFile('output.txt', 'w') as f:
        await f.write("Hello from async Python!")
    
    print("\n=== Real aiofiles usage would look like ===")
    code = '''
    import aiofiles
    
    async def read_file(path):
        async with aiofiles.open(path, 'r') as f:
            return await f.read()
    
    async def write_file(path, content):
        async with aiofiles.open(path, 'w') as f:
            await f.write(content)
    '''
    print(code)

asyncio.run(demo_async_file())
```

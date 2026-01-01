---
type: "THEORY"
title: "Syntax Breakdown: aiofiles API"
---

### Installation:
```bash
pip install aiofiles
```

### Basic Reading:
```python
import aiofiles

async def read_file(path):
    async with aiofiles.open(path, 'r') as f:
        content = await f.read()
    return content
```

### Basic Writing:
```python
async def write_file(path, content):
    async with aiofiles.open(path, 'w') as f:
        await f.write(content)
```

### Reading Lines:
```python
async def read_lines(path):
    async with aiofiles.open(path, 'r') as f:
        async for line in f:
            print(line.strip())
```

### Reading All Lines at Once:
```python
async def get_all_lines(path):
    async with aiofiles.open(path, 'r') as f:
        lines = await f.readlines()
    return lines
```

### File Modes (same as regular open):
- `'r'` - Read text
- `'w'` - Write text (overwrites)
- `'a'` - Append text
- `'rb'` - Read binary
- `'wb'` - Write binary

### Concurrent File Operations:
```python
async def process_many_files(paths):
    # Create tasks for all files
    tasks = [read_file(p) for p in paths]
    
    # Run all concurrently
    results = await asyncio.gather(*tasks)
    
    return results
```

### JSON with aiofiles:
```python
import json
import aiofiles

async def load_json(path):
    async with aiofiles.open(path, 'r') as f:
        content = await f.read()
    return json.loads(content)

async def save_json(path, data):
    async with aiofiles.open(path, 'w') as f:
        await f.write(json.dumps(data, indent=2))
```

### Error Handling:
```python
async def safe_read(path):
    try:
        async with aiofiles.open(path, 'r') as f:
            return await f.read()
    except FileNotFoundError:
        return None
    except PermissionError:
        print(f"Cannot read {path}")
        return None
```
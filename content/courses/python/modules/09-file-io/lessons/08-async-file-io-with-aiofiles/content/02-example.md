---
type: "EXAMPLE"
title: "Code Example: Async File Operations"
---

**Key patterns:**
1. `async with aiofiles.open()` - async context manager
2. `await f.read()` - async read
3. `await f.write()` - async write
4. `asyncio.gather()` - run multiple async operations concurrently

```python
# Note: Run this with Python 3.7+
# Install: pip install aiofiles

import asyncio
import aiofiles
from pathlib import Path
import json
from datetime import datetime

async def read_file_async(path: str) -> str:
    """Read a file asynchronously."""
    async with aiofiles.open(path, 'r') as f:
        return await f.read()

async def write_file_async(path: str, content: str) -> None:
    """Write to a file asynchronously."""
    async with aiofiles.open(path, 'w') as f:
        await f.write(content)

async def read_json_async(path: str) -> dict:
    """Read and parse JSON file asynchronously."""
    async with aiofiles.open(path, 'r') as f:
        content = await f.read()
        return json.loads(content)

async def write_json_async(path: str, data: dict) -> None:
    """Write data as JSON asynchronously."""
    async with aiofiles.open(path, 'w') as f:
        await f.write(json.dumps(data, indent=2))

async def process_multiple_files(file_paths: list) -> list:
    """Read multiple files concurrently."""
    tasks = [read_file_async(path) for path in file_paths]
    contents = await asyncio.gather(*tasks)
    return contents

async def demo_finance_tracker():
    """Demo: Async file I/O for Finance Tracker."""
    base = Path('async_demo')
    base.mkdir(exist_ok=True)
    
    # Create sample transaction files
    months = ['january', 'february', 'march']
    
    print("=== Writing Transaction Files (Async) ===")
    for month in months:
        transactions = {
            'month': month,
            'transactions': [
                {'date': '2024-01-15', 'amount': 50.00, 'category': 'food'},
                {'date': '2024-01-20', 'amount': 120.00, 'category': 'utilities'}
            ],
            'created': datetime.now().isoformat()
        }
        path = base / f'{month}_transactions.json'
        await write_json_async(str(path), transactions)
        print(f"  Wrote: {path.name}")
    
    print("\n=== Reading All Files Concurrently ===")
    file_paths = [str(base / f'{m}_transactions.json') for m in months]
    
    # Read all files at once
    start = datetime.now()
    all_data = await asyncio.gather(*[read_json_async(p) for p in file_paths])
    elapsed = (datetime.now() - start).total_seconds()
    
    for data in all_data:
        print(f"  {data['month']}: {len(data['transactions'])} transactions")
    
    print(f"\n  Read {len(all_data)} files in {elapsed:.3f}s")
    
    print("\n=== Async Line-by-Line Reading ===")
    # Append more data
    log_file = base / 'activity.log'
    await write_file_async(str(log_file), 'Line 1\nLine 2\nLine 3\n')
    
    async with aiofiles.open(str(log_file), 'r') as f:
        line_num = 0
        async for line in f:
            line_num += 1
            print(f"  Line {line_num}: {line.strip()}")

# Run the demo
if __name__ == '__main__':
    asyncio.run(demo_finance_tracker())
```

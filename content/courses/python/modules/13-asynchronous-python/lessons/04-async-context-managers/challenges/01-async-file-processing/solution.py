import asyncio
import time

class AsyncFileReader:
    """Simulates async file reading"""
    
    def __init__(self, filename):
        self.filename = filename
    
    async def __aenter__(self):
        print(f"  Opening {self.filename}...")
        await asyncio.sleep(0.2)  # Simulate open
        return self
    
    async def __aexit__(self, *args):
        print(f"  Closing {self.filename}...")
        await asyncio.sleep(0.1)  # Simulate close
    
    async def read(self):
        await asyncio.sleep(0.3)  # Simulate read
        return f"Content of {self.filename}"

async def read_file(filename):
    """Read a single file asynchronously"""
    async with AsyncFileReader(filename) as f:
        content = await f.read()
        return content

async def process_files(filenames):
    """Read multiple files concurrently"""
    # Create coroutines for each file
    coroutines = [read_file(f) for f in filenames]
    
    # Run all concurrently
    results = await asyncio.gather(*coroutines)
    
    return results

async def main():
    files = ["file1.txt", "file2.txt", "file3.txt"]
    
    print("Processing files concurrently...")
    start = time.time()
    
    contents = await process_files(files)
    
    elapsed = time.time() - start
    print(f"\nResults (took {elapsed:.2f}s):")
    for content in contents:
        print(f"  {content}")

asyncio.run(main())
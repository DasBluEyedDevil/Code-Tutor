import asyncio

class AsyncFileReader:
    """Simulates async file reading"""
    
    def __init__(self, filename):
        self.filename = filename
    
    async def __aenter__(self):
        await asyncio.sleep(0.2)  # Simulate open
        return self
    
    async def __aexit__(self, *args):
        await asyncio.sleep(0.1)  # Simulate close
    
    async def read(self):
        await asyncio.sleep(0.3)  # Simulate read
        return f"Content of {self.filename}"

async def read_file(filename):
    """Read a single file asynchronously"""
    # TODO: Use async with to read the file
    pass

async def process_files(filenames):
    """Read multiple files concurrently"""
    # TODO: Use asyncio.gather to read all files
    pass

async def main():
    files = ["file1.txt", "file2.txt", "file3.txt"]
    contents = await process_files(files)
    for content in contents:
        print(content)

asyncio.run(main())
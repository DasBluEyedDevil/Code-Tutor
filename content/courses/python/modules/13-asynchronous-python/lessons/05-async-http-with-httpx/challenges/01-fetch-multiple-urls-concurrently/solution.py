import asyncio
import time

# Mock client for demonstration
class MockClient:
    async def __aenter__(self):
        print("  Client opened")
        return self
    
    async def __aexit__(self, *args):
        print("  Client closed")
    
    async def get(self, url):
        print(f"  Fetching {url}...")
        await asyncio.sleep(0.2)
        return MockResponse(url)

class MockResponse:
    def __init__(self, url):
        self.url = url
        self.status_code = 200
    
    def json(self):
        return {"url": self.url, "data": "sample"}

async def fetch_urls(urls):
    """Fetch all URLs concurrently and return list of JSON data"""
    async with MockClient() as client:
        # Create list of coroutines
        coroutines = [client.get(url) for url in urls]
        
        # Fetch all concurrently
        responses = await asyncio.gather(*coroutines)
        
        # Extract JSON from each response
        return [response.json() for response in responses]

async def main():
    urls = [
        "https://api.example.com/users",
        "https://api.example.com/posts",
        "https://api.example.com/comments"
    ]
    
    print("Fetching URLs concurrently...")
    start = time.time()
    
    results = await fetch_urls(urls)
    
    elapsed = time.time() - start
    print(f"\nResults (took {elapsed:.2f}s):")
    for result in results:
        print(f"  {result}")

asyncio.run(main())
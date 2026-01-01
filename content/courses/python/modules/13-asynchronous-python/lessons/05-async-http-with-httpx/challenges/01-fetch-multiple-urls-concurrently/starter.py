import asyncio

# Mock client for demonstration
class MockClient:
    async def __aenter__(self): return self
    async def __aexit__(self, *args): pass
    
    async def get(self, url):
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
    # TODO: Use async with MockClient() as client
    # TODO: Use asyncio.gather to fetch all URLs
    # TODO: Return list of response.json() for each response
    pass

async def main():
    urls = [
        "https://api.example.com/users",
        "https://api.example.com/posts",
        "https://api.example.com/comments"
    ]
    
    results = await fetch_urls(urls)
    for result in results:
        print(result)

asyncio.run(main())
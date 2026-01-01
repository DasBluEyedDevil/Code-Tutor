---
type: "EXAMPLE"
title: "Making Concurrent HTTP Requests"
---

**Basic httpx usage:**
```python
import httpx

# Sync usage (like requests)
response = httpx.get('https://api.example.com/data')

# Async usage
async with httpx.AsyncClient() as client:
    response = await client.get('https://api.example.com/data')
```

**Connection pooling with AsyncClient:**
Always use `async with` to manage the client lifecycle!

```python
import asyncio

# Simulating httpx behavior (real httpx requires installation)
# pip install httpx

class MockAsyncClient:
    """Simulates httpx.AsyncClient for demonstration"""
    
    async def __aenter__(self):
        print("  [Client] Connection pool opened")
        return self
    
    async def __aexit__(self, *args):
        print("  [Client] Connection pool closed")
    
    async def get(self, url):
        print(f"  GET {url}")
        await asyncio.sleep(0.3)  # Simulate network
        return MockResponse(url, 200)
    
    async def post(self, url, json=None):
        print(f"  POST {url}")
        await asyncio.sleep(0.3)
        return MockResponse(url, 201, json)

class MockResponse:
    def __init__(self, url, status_code, data=None):
        self.url = url
        self.status_code = status_code
        self._data = data or {"source": url}
    
    def json(self):
        return self._data
    
    @property
    def text(self):
        return str(self._data)

async def demo_async_http():
    print("=== Async HTTP Requests ===")
    
    async with MockAsyncClient() as client:
        # Single request
        response = await client.get("https://api.example.com/users")
        print(f"  Status: {response.status_code}")
        print(f"  Data: {response.json()}\n")
        
        # Multiple concurrent requests
        print("=== Concurrent Requests ===")
        responses = await asyncio.gather(
            client.get("https://api.example.com/users"),
            client.get("https://api.example.com/posts"),
            client.get("https://api.example.com/comments")
        )
        
        for r in responses:
            print(f"  {r.url}: {r.status_code}")
    
    print("\n=== Real httpx usage ===")
    code = '''
    import httpx
    import asyncio
    
    async def fetch_all():
        async with httpx.AsyncClient() as client:
            responses = await asyncio.gather(
                client.get("https://api.github.com/users/python"),
                client.get("https://api.github.com/users/django"),
                client.get("https://api.github.com/users/fastapi")
            )
            return [r.json() for r in responses]
    
    users = asyncio.run(fetch_all())
    '''
    print(code)

asyncio.run(demo_async_http())
```

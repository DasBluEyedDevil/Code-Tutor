import asyncio
from dataclasses import dataclass
from typing import List, Tuple

@dataclass
class Result:
    url: str
    success: bool
    content: str = ""
    error: str = ""

class URLFetcher:
    def __init__(self, max_concurrent: int = 3):
        self.semaphore = asyncio.Semaphore(max_concurrent)
    
    async def fetch_one(self, url: str) -> Result:
        """Fetch a single URL. Handle errors gracefully."""
        async with self.semaphore:
            try:
                print(f"  Fetching {url}...")
                await asyncio.sleep(0.2)  # Simulate network delay
                
                # Simulate failure for URLs containing 'error'
                if 'error' in url:
                    raise ConnectionError(f"Failed to connect to {url}")
                
                return Result(
                    url=url,
                    success=True,
                    content=f"Data from {url}"
                )
            
            except Exception as e:
                return Result(
                    url=url,
                    success=False,
                    error=str(e)
                )
    
    async def fetch_all(self, urls: List[str]) -> Tuple[List[Result], List[Result]]:
        """Fetch all URLs. Return (successes, failures)."""
        # Fetch all concurrently
        results = await asyncio.gather(
            *[self.fetch_one(url) for url in urls]
        )
        
        # Separate into successes and failures
        successes = [r for r in results if r.success]
        failures = [r for r in results if not r.success]
        
        return successes, failures

async def main():
    urls = [
        "https://api.example.com/data",
        "https://error.example.com/fail",
        "https://api.example.com/users",
        "https://api.example.com/posts",
        "https://error.example.com/broken",
    ]
    
    print("=== URL Fetcher Demo ===")
    print(f"Fetching {len(urls)} URLs with max 2 concurrent...\n")
    
    fetcher = URLFetcher(max_concurrent=2)
    successes, failures = await fetcher.fetch_all(urls)
    
    print(f"\n=== Results ===")
    print(f"Successes: {len(successes)}")
    for r in successes:
        print(f"  OK: {r.url}")
    
    print(f"\nFailures: {len(failures)}")
    for r in failures:
        print(f"  FAIL: {r.url} - {r.error}")

asyncio.run(main())
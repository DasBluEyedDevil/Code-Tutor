---
type: "EXAMPLE"
title: "Complete Async Scraper with Rate Limiting"
---

**Key components:**
1. Semaphore for concurrency control
2. Error handling for each request
3. Progress tracking
4. Respectful delays between requests

```python
import asyncio
import time
from dataclasses import dataclass
from typing import List, Optional

@dataclass
class FetchResult:
    url: str
    success: bool
    data: Optional[str] = None
    error: Optional[str] = None

class AsyncScraper:
    """Async web scraper with rate limiting and error handling"""
    
    def __init__(self, max_concurrent: int = 5, delay: float = 0.1):
        self.semaphore = asyncio.Semaphore(max_concurrent)
        self.delay = delay
        self.completed = 0
        self.total = 0
    
    async def fetch_one(self, url: str) -> FetchResult:
        """Fetch a single URL with rate limiting"""
        async with self.semaphore:  # Limit concurrency
            try:
                # Simulate network request
                await asyncio.sleep(0.2 + (hash(url) % 10) / 100)
                
                # Simulate occasional failures
                if "fail" in url:
                    raise ConnectionError(f"Failed to connect to {url}")
                
                self.completed += 1
                print(f"  [{self.completed}/{self.total}] Fetched {url}")
                
                # Be respectful - add delay between requests
                await asyncio.sleep(self.delay)
                
                return FetchResult(
                    url=url,
                    success=True,
                    data=f"Content from {url}"
                )
            
            except Exception as e:
                self.completed += 1
                print(f"  [{self.completed}/{self.total}] FAILED {url}: {e}")
                return FetchResult(
                    url=url,
                    success=False,
                    error=str(e)
                )
    
    async def fetch_all(self, urls: List[str]) -> List[FetchResult]:
        """Fetch all URLs concurrently with rate limiting"""
        self.total = len(urls)
        self.completed = 0
        
        print(f"\nStarting fetch of {self.total} URLs...")
        print(f"Max concurrent: {self.semaphore._value}")
        print(f"Delay between requests: {self.delay}s\n")
        
        # Create tasks for all URLs
        tasks = [self.fetch_one(url) for url in urls]
        
        # Run all with concurrency control
        results = await asyncio.gather(*tasks)
        
        return results

async def main():
    print("=== Async Web Scraper Demo ===")
    
    # Sample URLs (some will fail)
    urls = [
        "https://example.com/page1",
        "https://example.com/page2",
        "https://fail.com/page",  # Will fail
        "https://example.com/page3",
        "https://example.com/page4",
        "https://fail.com/other",  # Will fail
        "https://example.com/page5",
        "https://example.com/page6",
    ]
    
    # Create scraper with limits
    scraper = AsyncScraper(
        max_concurrent=3,  # Only 3 at a time
        delay=0.1  # 100ms between requests
    )
    
    start = time.time()
    results = await scraper.fetch_all(urls)
    elapsed = time.time() - start
    
    # Summary
    successes = [r for r in results if r.success]
    failures = [r for r in results if not r.success]
    
    print(f"\n=== Results ===")
    print(f"Total time: {elapsed:.2f}s")
    print(f"Successful: {len(successes)}")
    print(f"Failed: {len(failures)}")
    
    if failures:
        print(f"\nFailed URLs:")
        for f in failures:
            print(f"  - {f.url}: {f.error}")

print("Async Scraper with Rate Limiting\n")
asyncio.run(main())
```

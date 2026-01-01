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
        # TODO: Use semaphore for rate limiting
        # TODO: Simulate fetch with asyncio.sleep(0.2)
        # TODO: Simulate failure if 'error' in url
        # TODO: Return Result with success/failure info
        pass
    
    async def fetch_all(self, urls: List[str]) -> Tuple[List[Result], List[Result]]:
        """Fetch all URLs. Return (successes, failures)."""
        # TODO: Use asyncio.gather to fetch all
        # TODO: Separate results into successes and failures
        pass

async def main():
    urls = [
        "https://api.example.com/data",
        "https://error.example.com/fail",
        "https://api.example.com/users",
        "https://api.example.com/posts",
        "https://error.example.com/broken",
    ]
    
    fetcher = URLFetcher(max_concurrent=2)
    successes, failures = await fetcher.fetch_all(urls)
    
    print(f"Successes: {len(successes)}")
    print(f"Failures: {len(failures)}")

asyncio.run(main())
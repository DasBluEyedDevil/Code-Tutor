---
type: "KEY_POINT"
title: "Be Respectful - Add Delays Between Requests"
---

**Web scraping etiquette:**

1. **Rate limit your requests**
   - Use semaphores to limit concurrent connections
   - Add delays between requests

2. **Handle errors gracefully**
   - Sites go down, connections fail
   - Don't crash on individual failures

3. **Respect robots.txt**
   - Check if scraping is allowed
   - Some sites block scrapers

4. **Identify yourself**
   - Set a proper User-Agent header
   - Include contact info if possible

**Good scraper pattern:**
```python
class RespectfulScraper:
    def __init__(self):
        self.semaphore = asyncio.Semaphore(5)
        self.delay = 1.0  # 1 second between requests
    
    async def fetch(self, url):
        async with self.semaphore:
            try:
                result = await self.client.get(url)
                await asyncio.sleep(self.delay)
                return result
            except Exception as e:
                return None
```
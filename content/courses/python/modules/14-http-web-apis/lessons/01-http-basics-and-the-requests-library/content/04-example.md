---
type: "EXAMPLE"
title: "Code Example: Working with Real APIs"
---

**Building API clients - best practices:**

**1. Use sessions:**
```python
session = requests.Session()
session.headers['Authorization'] = 'token'
# Reuses connection, faster!
```

**2. Error handling:**
```python
try:
    response.raise_for_status()
except requests.exceptions.HTTPError:
    # Handle 4xx/5xx
except requests.exceptions.Timeout:
    # Handle timeout
```

**3. Caching:**
- Store responses temporarily
- Reduce API calls
- Faster for repeated requests

**4. Rate limiting:**
- Respect API limits
- Avoid being blocked
- Track request timestamps

**5. Type hints:**
```python
def get_user(username: str) -> Optional[Dict]:
    ...
```
Clear what function expects/returns

```python
import requests
from typing import Optional, Dict, List

print("=== GitHub API Example ===")

class GitHubAPI:
    """Simple GitHub API client"""
    
    BASE_URL = 'https://api.github.com'
    
    def __init__(self, token: Optional[str] = None):
        self.session = requests.Session()
        if token:
            self.session.headers['Authorization'] = f'token {token}'
        self.session.headers['Accept'] = 'application/vnd.github.v3+json'
    
    def get_user(self, username: str) -> Optional[Dict]:
        """Get user information"""
        response = self.session.get(f'{self.BASE_URL}/users/{username}')
        
        if response.ok:
            return response.json()
        return None
    
    def get_repos(self, username: str, limit: int = 5) -> List[Dict]:
        """Get user repositories"""
        response = self.session.get(
            f'{self.BASE_URL}/users/{username}/repos',
            params={'sort': 'updated', 'per_page': limit}
        )
        
        if response.ok:
            return response.json()
        return []

# Use the API client
gh = GitHubAPI()

# Get user info
user = gh.get_user('octocat')
if user:
    print(f"User: {user['login']}")
    print(f"Name: {user['name']}")
    print(f"Public Repos: {user['public_repos']}")
    print(f"Followers: {user['followers']}")

# Get repositories
repos = gh.get_repos('octocat', limit=3)
print(f"\nTop {len(repos)} repositories:")
for repo in repos:
    print(f"  - {repo['name']}: {repo['description'] or 'No description'}")
    print(f"    ⭐ {repo['stargazers_count']} stars")

print("\n=== Weather API Example (OpenWeatherMap concept) ===")

class WeatherAPI:
    """Weather API client (example structure)"""
    
    def __init__(self, api_key: str):
        self.api_key = api_key
        self.base_url = 'https://api.openweathermap.org/data/2.5'
    
    def get_current_weather(self, city: str) -> Optional[Dict]:
        """Get current weather for a city"""
        params = {
            'q': city,
            'appid': self.api_key,
            'units': 'metric'
        }
        
        try:
            response = requests.get(
                f'{self.base_url}/weather',
                params=params,
                timeout=10
            )
            response.raise_for_status()
            return response.json()
        except requests.exceptions.RequestException as e:
            print(f"Error fetching weather: {e}")
            return None

# Example usage (would need real API key)
print("Weather API structure example (requires API key)")
print("  weather = WeatherAPI('your-api-key')")
print("  data = weather.get_current_weather('London')")

print("\n=== API Response Caching ===")

import time
from functools import lru_cache

class CachedAPI:
    """API client with caching"""
    
    def __init__(self):
        self.cache = {}
        self.cache_duration = 300  # 5 minutes
    
    def get_data(self, url: str) -> Optional[Dict]:
        """Get data with caching"""
        now = time.time()
        
        # Check cache
        if url in self.cache:
            cached_data, timestamp = self.cache[url]
            if now - timestamp < self.cache_duration:
                print(f"  ✓ Cache hit for {url}")
                return cached_data
        
        # Make request
        print(f"  → Making request to {url}")
        try:
            response = requests.get(url, timeout=5)
            response.raise_for_status()
            data = response.json()
            
            # Cache the result
            self.cache[url] = (data, now)
            return data
        except requests.exceptions.RequestException as e:
            print(f"  ✗ Error: {e}")
            return None

api = CachedAPI()

url = 'https://jsonplaceholder.typicode.com/users/1'

print("First request (will fetch):")
data1 = api.get_data(url)
if data1:
    print(f"  Got: {data1['name']}")

print("\nSecond request (will use cache):")
data2 = api.get_data(url)
if data2:
    print(f"  Got: {data2['name']}")

print("\n=== Rate Limiting ===")

import time

class RateLimitedAPI:
    """API client with rate limiting"""
    
    def __init__(self, requests_per_minute: int = 60):
        self.requests_per_minute = requests_per_minute
        self.request_times = []
    
    def _wait_if_needed(self):
        """Wait if rate limit would be exceeded"""
        now = time.time()
        
        # Remove requests older than 1 minute
        self.request_times = [
            t for t in self.request_times 
            if now - t < 60
        ]
        
        # Wait if at limit
        if len(self.request_times) >= self.requests_per_minute:
            wait_time = 60 - (now - self.request_times[0])
            if wait_time > 0:
                print(f"  Rate limit reached, waiting {wait_time:.1f}s...")
                time.sleep(wait_time)
                self.request_times = []
    
    def get(self, url: str) -> Optional[requests.Response]:
        """Make rate-limited GET request"""
        self._wait_if_needed()
        self.request_times.append(time.time())
        return requests.get(url)

api = RateLimitedAPI(requests_per_minute=2)

print("Making rate-limited requests:")
for i in range(3):
    print(f"  Request {i+1}")
    response = api.get('https://jsonplaceholder.typicode.com/users/1')
    print(f"    Status: {response.status_code}")
```

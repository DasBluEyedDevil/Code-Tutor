# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** HTTP & Web APIs
- **Lesson:** HTTP Basics and the Requests Library (ID: 14_01)
- **Difficulty:** advanced
- **Estimated Time:** 35 minutes

## Current Lesson Content

{
    "id":  "14_01",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: How the Web Works",
                                "content":  "**HTTP = How computers talk on the web**\n\n**Think of it like a restaurant:**\n\n**Client (You) → Server (Kitchen)**\n- You: \"Can I have a burger?\" (REQUEST)\n- Kitchen: \"Here\u0027s your burger!\" (RESPONSE)\n\n**HTTP Request Methods:**\n\n1. **GET** 📖 - Read/retrieve data\n   - Like asking to see the menu\n   - \"Show me the user with ID 5\"\n\n2. **POST** ➕ - Create new data\n   - Like placing an order\n   - \"Create a new user account\"\n\n3. **PUT** 📝 - Update existing data\n   - Like changing your order\n   - \"Update user profile\"\n\n4. **DELETE** 🗑️ - Remove data\n   - Like canceling your order\n   - \"Delete this post\"\n\n**HTTP Status Codes:**\n- **2xx (Success)** ✅\n  - 200: OK - Request succeeded\n  - 201: Created - New resource created\n\n- **3xx (Redirect)** ↪️\n  - 301: Moved permanently\n  - 302: Temporary redirect\n\n- **4xx (Client Error)** ❌\n  - 400: Bad request\n  - 401: Unauthorized\n  - 404: Not found\n\n- **5xx (Server Error)** 💥\n  - 500: Internal server error\n  - 503: Service unavailable\n\n**Headers:** Metadata about the request/response\n- Content-Type: application/json\n- Authorization: Bearer token\n- User-Agent: Browser info"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Making HTTP Requests",
                                "content":  "**requests library key features:**\n\n**1. HTTP Methods:**\n```python\nrequests.get(url)     # Retrieve\nrequests.post(url)    # Create\nrequests.put(url)     # Update\nrequests.delete(url)  # Remove\n```\n\n**2. Response object:**\n```python\nresponse.status_code  # 200, 404, etc.\nresponse.ok           # True if 2xx\nresponse.json()       # Parse JSON\nresponse.text         # Raw text\nresponse.headers      # Response headers\n```\n\n**3. Sending data:**\n```python\n# JSON data\nrequests.post(url, json={\u0027key\u0027: \u0027value\u0027})\n\n# Form data\nrequests.post(url, data={\u0027key\u0027: \u0027value\u0027})\n\n# Query parameters\nrequests.get(url, params={\u0027key\u0027: \u0027value\u0027})\n```\n\n**4. Headers:**\n```python\nheaders = {\u0027Authorization\u0027: \u0027Bearer token\u0027}\nrequests.get(url, headers=headers)\n```",
                                "code":  "import requests\nimport json\n\nprint(\"=== GET Request - Retrieve Data ===\")\n\n# Simple GET request\nresponse = requests.get(\u0027https://jsonplaceholder.typicode.com/users/1\u0027)\n\nprint(f\"Status Code: {response.status_code}\")\nprint(f\"Success: {response.ok}\")\nprint(f\"\\nResponse Headers:\")\nfor key, value in list(response.headers.items())[:5]:\n    print(f\"  {key}: {value}\")\n\n# Parse JSON response\nuser = response.json()\nprint(f\"\\nUser Data:\")\nprint(f\"  Name: {user[\u0027name\u0027]}\")\nprint(f\"  Email: {user[\u0027email\u0027]}\")\nprint(f\"  City: {user[\u0027address\u0027][\u0027city\u0027]}\")\n\nprint(\"\\n=== GET with Query Parameters ===\")\n\n# Get multiple users\nparams = {\n    \u0027_limit\u0027: 3,\n    \u0027_sort\u0027: \u0027name\u0027\n}\n\nresponse = requests.get(\n    \u0027https://jsonplaceholder.typicode.com/users\u0027,\n    params=params\n)\n\nusers = response.json()\nprint(f\"Retrieved {len(users)} users:\")\nfor user in users:\n    print(f\"  - {user[\u0027name\u0027]} ({user[\u0027email\u0027]})\")\n\nprint(\"\\n=== POST Request - Create Data ===\")\n\n# Create new post\nnew_post = {\n    \u0027title\u0027: \u0027My First Post\u0027,\n    \u0027body\u0027: \u0027This is the content of my post\u0027,\n    \u0027userId\u0027: 1\n}\n\nresponse = requests.post(\n    \u0027https://jsonplaceholder.typicode.com/posts\u0027,\n    json=new_post\n)\n\nprint(f\"Status Code: {response.status_code}\")\ncreated_post = response.json()\nprint(f\"Created Post:\")\nprint(f\"  ID: {created_post.get(\u0027id\u0027)}\")\nprint(f\"  Title: {created_post[\u0027title\u0027]}\")\nprint(f\"  Body: {created_post[\u0027body\u0027]}\")\n\nprint(\"\\n=== PUT Request - Update Data ===\")\n\n# Update existing post\nupdated_post = {\n    \u0027id\u0027: 1,\n    \u0027title\u0027: \u0027Updated Title\u0027,\n    \u0027body\u0027: \u0027Updated content\u0027,\n    \u0027userId\u0027: 1\n}\n\nresponse = requests.put(\n    \u0027https://jsonplaceholder.typicode.com/posts/1\u0027,\n    json=updated_post\n)\n\nprint(f\"Status Code: {response.status_code}\")\nresult = response.json()\nprint(f\"Updated Post:\")\nprint(f\"  Title: {result[\u0027title\u0027]}\")\nprint(f\"  Body: {result[\u0027body\u0027]}\")\n\nprint(\"\\n=== DELETE Request - Remove Data ===\")\n\nresponse = requests.delete(\u0027https://jsonplaceholder.typicode.com/posts/1\u0027)\n\nprint(f\"Status Code: {response.status_code}\")\nprint(f\"Deletion successful: {response.status_code == 200}\")\n\nprint(\"\\n=== Custom Headers ===\")\n\nheaders = {\n    \u0027User-Agent\u0027: \u0027Python Training Course/1.0\u0027,\n    \u0027Accept\u0027: \u0027application/json\u0027,\n    \u0027Custom-Header\u0027: \u0027Custom Value\u0027\n}\n\nresponse = requests.get(\n    \u0027https://httpbin.org/headers\u0027,\n    headers=headers\n)\n\nreceived_headers = response.json()\nprint(\"Headers sent and received:\")\nfor key, value in received_headers[\u0027headers\u0027].items():\n    print(f\"  {key}: {value}\")\n\nprint(\"\\n=== Error Handling ===\")\n\ntry:\n    response = requests.get(\u0027https://jsonplaceholder.typicode.com/users/999\u0027)\n    response.raise_for_status()  # Raises exception for 4xx/5xx\n    \n    user = response.json()\n    if not user:\n        print(\"User not found (empty response)\")\n    else:\n        print(f\"Found: {user[\u0027name\u0027]}\")\nexcept requests.exceptions.HTTPError as e:\n    print(f\"HTTP Error: {e}\")\nexcept requests.exceptions.RequestException as e:\n    print(f\"Request Error: {e}\")\n\nprint(\"\\n=== Timeout and Retries ===\")\n\ntry:\n    response = requests.get(\n        \u0027https://jsonplaceholder.typicode.com/users/1\u0027,\n        timeout=5  # 5 second timeout\n    )\n    print(f\"Request completed in {response.elapsed.total_seconds():.3f}s\")\nexcept requests.exceptions.Timeout:\n    print(\"Request timed out!\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Syntax Breakdown",
                                "content":  "**Basic request pattern:**\n```python\nimport requests\n\n# GET request\nresponse = requests.get(url)\n\n# Check status\nif response.ok:  # or response.status_code == 200\n    data = response.json()\n```\n\n**With parameters:**\n```python\n# Query parameters (?key=value\u0026key2=value2)\nparams = {\u0027key\u0027: \u0027value\u0027, \u0027key2\u0027: \u0027value2\u0027}\nresponse = requests.get(url, params=params)\n\n# POST with JSON data\ndata = {\u0027name\u0027: \u0027Alice\u0027, \u0027age\u0027: 25}\nresponse = requests.post(url, json=data)\n\n# Custom headers\nheaders = {\u0027Authorization\u0027: \u0027Bearer token\u0027}\nresponse = requests.get(url, headers=headers)\n```\n\n**Error handling:**\n```python\ntry:\n    response = requests.get(url, timeout=5)\n    response.raise_for_status()  # Raise exception for errors\n    data = response.json()\nexcept requests.exceptions.HTTPError:\n    print(\"HTTP error\")\nexcept requests.exceptions.Timeout:\n    print(\"Timeout\")\nexcept requests.exceptions.RequestException:\n    print(\"Request failed\")\n```\n\n**Common patterns:**\n```python\n# Check status\nif response.status_code == 200:\n    # Success\n    pass\n\n# Parse response\njson_data = response.json()  # For JSON\ntext_data = response.text    # For text\nbinary = response.content    # For binary\n```"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Working with Real APIs",
                                "content":  "**Building API clients - best practices:**\n\n**1. Use sessions:**\n```python\nsession = requests.Session()\nsession.headers[\u0027Authorization\u0027] = \u0027token\u0027\n# Reuses connection, faster!\n```\n\n**2. Error handling:**\n```python\ntry:\n    response.raise_for_status()\nexcept requests.exceptions.HTTPError:\n    # Handle 4xx/5xx\nexcept requests.exceptions.Timeout:\n    # Handle timeout\n```\n\n**3. Caching:**\n- Store responses temporarily\n- Reduce API calls\n- Faster for repeated requests\n\n**4. Rate limiting:**\n- Respect API limits\n- Avoid being blocked\n- Track request timestamps\n\n**5. Type hints:**\n```python\ndef get_user(username: str) -\u003e Optional[Dict]:\n    ...\n```\nClear what function expects/returns",
                                "code":  "import requests\nfrom typing import Optional, Dict, List\n\nprint(\"=== GitHub API Example ===\")\n\nclass GitHubAPI:\n    \"\"\"Simple GitHub API client\"\"\"\n    \n    BASE_URL = \u0027https://api.github.com\u0027\n    \n    def __init__(self, token: Optional[str] = None):\n        self.session = requests.Session()\n        if token:\n            self.session.headers[\u0027Authorization\u0027] = f\u0027token {token}\u0027\n        self.session.headers[\u0027Accept\u0027] = \u0027application/vnd.github.v3+json\u0027\n    \n    def get_user(self, username: str) -\u003e Optional[Dict]:\n        \"\"\"Get user information\"\"\"\n        response = self.session.get(f\u0027{self.BASE_URL}/users/{username}\u0027)\n        \n        if response.ok:\n            return response.json()\n        return None\n    \n    def get_repos(self, username: str, limit: int = 5) -\u003e List[Dict]:\n        \"\"\"Get user repositories\"\"\"\n        response = self.session.get(\n            f\u0027{self.BASE_URL}/users/{username}/repos\u0027,\n            params={\u0027sort\u0027: \u0027updated\u0027, \u0027per_page\u0027: limit}\n        )\n        \n        if response.ok:\n            return response.json()\n        return []\n\n# Use the API client\ngh = GitHubAPI()\n\n# Get user info\nuser = gh.get_user(\u0027octocat\u0027)\nif user:\n    print(f\"User: {user[\u0027login\u0027]}\")\n    print(f\"Name: {user[\u0027name\u0027]}\")\n    print(f\"Public Repos: {user[\u0027public_repos\u0027]}\")\n    print(f\"Followers: {user[\u0027followers\u0027]}\")\n\n# Get repositories\nrepos = gh.get_repos(\u0027octocat\u0027, limit=3)\nprint(f\"\\nTop {len(repos)} repositories:\")\nfor repo in repos:\n    print(f\"  - {repo[\u0027name\u0027]}: {repo[\u0027description\u0027] or \u0027No description\u0027}\")\n    print(f\"    ⭐ {repo[\u0027stargazers_count\u0027]} stars\")\n\nprint(\"\\n=== Weather API Example (OpenWeatherMap concept) ===\")\n\nclass WeatherAPI:\n    \"\"\"Weather API client (example structure)\"\"\"\n    \n    def __init__(self, api_key: str):\n        self.api_key = api_key\n        self.base_url = \u0027https://api.openweathermap.org/data/2.5\u0027\n    \n    def get_current_weather(self, city: str) -\u003e Optional[Dict]:\n        \"\"\"Get current weather for a city\"\"\"\n        params = {\n            \u0027q\u0027: city,\n            \u0027appid\u0027: self.api_key,\n            \u0027units\u0027: \u0027metric\u0027\n        }\n        \n        try:\n            response = requests.get(\n                f\u0027{self.base_url}/weather\u0027,\n                params=params,\n                timeout=10\n            )\n            response.raise_for_status()\n            return response.json()\n        except requests.exceptions.RequestException as e:\n            print(f\"Error fetching weather: {e}\")\n            return None\n\n# Example usage (would need real API key)\nprint(\"Weather API structure example (requires API key)\")\nprint(\"  weather = WeatherAPI(\u0027your-api-key\u0027)\")\nprint(\"  data = weather.get_current_weather(\u0027London\u0027)\")\n\nprint(\"\\n=== API Response Caching ===\")\n\nimport time\nfrom functools import lru_cache\n\nclass CachedAPI:\n    \"\"\"API client with caching\"\"\"\n    \n    def __init__(self):\n        self.cache = {}\n        self.cache_duration = 300  # 5 minutes\n    \n    def get_data(self, url: str) -\u003e Optional[Dict]:\n        \"\"\"Get data with caching\"\"\"\n        now = time.time()\n        \n        # Check cache\n        if url in self.cache:\n            cached_data, timestamp = self.cache[url]\n            if now - timestamp \u003c self.cache_duration:\n                print(f\"  ✓ Cache hit for {url}\")\n                return cached_data\n        \n        # Make request\n        print(f\"  → Making request to {url}\")\n        try:\n            response = requests.get(url, timeout=5)\n            response.raise_for_status()\n            data = response.json()\n            \n            # Cache the result\n            self.cache[url] = (data, now)\n            return data\n        except requests.exceptions.RequestException as e:\n            print(f\"  ✗ Error: {e}\")\n            return None\n\napi = CachedAPI()\n\nurl = \u0027https://jsonplaceholder.typicode.com/users/1\u0027\n\nprint(\"First request (will fetch):\")\ndata1 = api.get_data(url)\nif data1:\n    print(f\"  Got: {data1[\u0027name\u0027]}\")\n\nprint(\"\\nSecond request (will use cache):\")\ndata2 = api.get_data(url)\nif data2:\n    print(f\"  Got: {data2[\u0027name\u0027]}\")\n\nprint(\"\\n=== Rate Limiting ===\")\n\nimport time\n\nclass RateLimitedAPI:\n    \"\"\"API client with rate limiting\"\"\"\n    \n    def __init__(self, requests_per_minute: int = 60):\n        self.requests_per_minute = requests_per_minute\n        self.request_times = []\n    \n    def _wait_if_needed(self):\n        \"\"\"Wait if rate limit would be exceeded\"\"\"\n        now = time.time()\n        \n        # Remove requests older than 1 minute\n        self.request_times = [\n            t for t in self.request_times \n            if now - t \u003c 60\n        ]\n        \n        # Wait if at limit\n        if len(self.request_times) \u003e= self.requests_per_minute:\n            wait_time = 60 - (now - self.request_times[0])\n            if wait_time \u003e 0:\n                print(f\"  Rate limit reached, waiting {wait_time:.1f}s...\")\n                time.sleep(wait_time)\n                self.request_times = []\n    \n    def get(self, url: str) -\u003e Optional[requests.Response]:\n        \"\"\"Make rate-limited GET request\"\"\"\n        self._wait_if_needed()\n        self.request_times.append(time.time())\n        return requests.get(url)\n\napi = RateLimitedAPI(requests_per_minute=2)\n\nprint(\"Making rate-limited requests:\")\nfor i in range(3):\n    print(f\"  Request {i+1}\")\n    response = api.get(\u0027https://jsonplaceholder.typicode.com/users/1\u0027)\n    print(f\"    Status: {response.status_code}\")",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **HTTP methods: GET (read), POST (create), PUT (update), DELETE (remove)**\n- **requests library makes HTTP easy** - requests.get(), .post(), etc.\n- **Always handle errors** - Network can fail, use try/except\n- **response.json() parses JSON** - Most APIs return JSON\n- **Use sessions for multiple requests** - Faster, maintains headers\n- **Set timeouts** - Prevent hanging: timeout=10\n- **Check response.ok or status_code** - Don\u0027t assume success\n- **Type hints improve API clients** - Clear interfaces and documentation\n- **For async: use httpx instead** - Drop-in replacement with `async with httpx.AsyncClient() as client: await client.get(url)` - same API as requests but supports async/await"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "13_01-challenge-4",
                           "title":  "Interactive Exercise",
                           "description":  "Create a simple API client for JSONPlaceholder that:\n- Gets all posts for a specific user\n- Filters posts by keyword in title\n- Returns formatted results\nImplement error handling and use type hints.",
                           "instructions":  "Create a simple API client for JSONPlaceholder that:\n- Gets all posts for a specific user\n- Filters posts by keyword in title\n- Returns formatted results\nImplement error handling and use type hints.",
                           "starterCode":  "import requests\nfrom typing import List, Dict, Optional\n\nclass PostsAPI:\n    BASE_URL = \u0027https://jsonplaceholder.typicode.com\u0027\n    \n    def get_user_posts(self, user_id: int) -\u003e List[Dict]:\n        # TODO: Get all posts for user_id\n        pass\n    \n    def filter_posts_by_keyword(self, posts: List[Dict], keyword: str) -\u003e List[Dict]:\n        # TODO: Filter posts where keyword is in title\n        pass\n    \n    def format_post(self, post: Dict) -\u003e str:\n        # TODO: Return formatted string \"[ID] Title\"\n        pass\n\n# Test\napi = PostsAPI()\nposts = api.get_user_posts(1)\nfiltered = api.filter_posts_by_keyword(posts, \u0027sunt\u0027)\nfor post in filtered:\n    print(api.format_post(post))",
                           "solution":  "import requests\nfrom typing import List, Dict, Optional\n\n# JSONPlaceholder API Client\n# This solution demonstrates API consumption with error handling\n\nclass PostsAPI:\n    \"\"\"API client for JSONPlaceholder posts.\"\"\"\n    \n    BASE_URL = \u0027https://jsonplaceholder.typicode.com\u0027\n    \n    def get_user_posts(self, user_id: int) -\u003e List[Dict]:\n        \"\"\"Get all posts for a specific user.\"\"\"\n        try:\n            url = f\"{self.BASE_URL}/posts\"\n            response = requests.get(url, params={\u0027userId\u0027: user_id})\n            response.raise_for_status()  # Raise exception for HTTP errors\n            return response.json()\n        except requests.RequestException as e:\n            print(f\"Error fetching posts: {e}\")\n            return []\n    \n    def filter_posts_by_keyword(self, posts: List[Dict], keyword: str) -\u003e List[Dict]:\n        \"\"\"Filter posts where keyword appears in title (case-insensitive).\"\"\"\n        keyword_lower = keyword.lower()\n        return [\n            post for post in posts\n            if keyword_lower in post.get(\u0027title\u0027, \u0027\u0027).lower()\n        ]\n    \n    def format_post(self, post: Dict) -\u003e str:\n        \"\"\"Return formatted string for a post.\"\"\"\n        post_id = post.get(\u0027id\u0027, \u0027?\u0027)\n        title = post.get(\u0027title\u0027, \u0027No title\u0027)\n        return f\"[{post_id}] {title}\"\n    \n    def get_post_summary(self, user_id: int) -\u003e Dict:\n        \"\"\"Get summary of user\u0027s posts.\"\"\"\n        posts = self.get_user_posts(user_id)\n        return {\n            \u0027user_id\u0027: user_id,\n            \u0027total_posts\u0027: len(posts),\n            \u0027posts\u0027: posts\n        }\n\n# Test the API client\nprint(\"=== Posts API Client Demo ===\")\n\napi = PostsAPI()\n\n# Get posts for user 1\nprint(\"\\nFetching posts for user 1...\")\nposts = api.get_user_posts(1)\nprint(f\"Found {len(posts)} posts\")\n\n# Filter by keyword\nprint(\"\\nFiltering posts with \u0027sunt\u0027 in title:\")\nfiltered = api.filter_posts_by_keyword(posts, \u0027sunt\u0027)\nfor post in filtered:\n    print(f\"  {api.format_post(post)}\")\n\n# Show first 3 posts\nprint(\"\\nFirst 3 posts:\")\nfor post in posts[:3]:\n    print(f\"  {api.format_post(post)}\")",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use requests.get with params={\u0027userId\u0027: user_id}. Filter with list comprehension checking if keyword in post[\u0027title\u0027]. Format with f-string."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the colon after if/for/while",
                                                      "consequence":  "SyntaxError",
                                                      "correction":  "Add : at the end of the line"
                                                  },
                                                  {
                                                      "mistake":  "Using = instead of == for comparison",
                                                      "consequence":  "Assignment instead of comparison",
                                                      "correction":  "Use == for equality checks"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect indentation",
                                                      "consequence":  "IndentationError",
                                                      "correction":  "Use consistent 4-space indentation"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "HTTP Basics and the Requests Library",
    "estimatedMinutes":  35
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python HTTP Basics and the Requests Library 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "14_01",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```


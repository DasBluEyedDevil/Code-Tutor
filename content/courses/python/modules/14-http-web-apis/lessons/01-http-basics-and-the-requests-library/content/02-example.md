---
type: "EXAMPLE"
title: "Code Example: Making HTTP Requests"
---

**requests library key features:**

**1. HTTP Methods:**
```python
requests.get(url)     # Retrieve
requests.post(url)    # Create
requests.put(url)     # Update
requests.delete(url)  # Remove
```

**2. Response object:**
```python
response.status_code  # 200, 404, etc.
response.ok           # True if 2xx
response.json()       # Parse JSON
response.text         # Raw text
response.headers      # Response headers
```

**3. Sending data:**
```python
# JSON data
requests.post(url, json={'key': 'value'})

# Form data
requests.post(url, data={'key': 'value'})

# Query parameters
requests.get(url, params={'key': 'value'})
```

**4. Headers:**
```python
headers = {'Authorization': 'Bearer token'}
requests.get(url, headers=headers)
```

```python
import requests
import json

print("=== GET Request - Retrieve Data ===")

# Simple GET request
response = requests.get('https://jsonplaceholder.typicode.com/users/1')

print(f"Status Code: {response.status_code}")
print(f"Success: {response.ok}")
print(f"\nResponse Headers:")
for key, value in list(response.headers.items())[:5]:
    print(f"  {key}: {value}")

# Parse JSON response
user = response.json()
print(f"\nUser Data:")
print(f"  Name: {user['name']}")
print(f"  Email: {user['email']}")
print(f"  City: {user['address']['city']}")

print("\n=== GET with Query Parameters ===")

# Get multiple users
params = {
    '_limit': 3,
    '_sort': 'name'
}

response = requests.get(
    'https://jsonplaceholder.typicode.com/users',
    params=params
)

users = response.json()
print(f"Retrieved {len(users)} users:")
for user in users:
    print(f"  - {user['name']} ({user['email']})")

print("\n=== POST Request - Create Data ===")

# Create new post
new_post = {
    'title': 'My First Post',
    'body': 'This is the content of my post',
    'userId': 1
}

response = requests.post(
    'https://jsonplaceholder.typicode.com/posts',
    json=new_post
)

print(f"Status Code: {response.status_code}")
created_post = response.json()
print(f"Created Post:")
print(f"  ID: {created_post.get('id')}")
print(f"  Title: {created_post['title']}")
print(f"  Body: {created_post['body']}")

print("\n=== PUT Request - Update Data ===")

# Update existing post
updated_post = {
    'id': 1,
    'title': 'Updated Title',
    'body': 'Updated content',
    'userId': 1
}

response = requests.put(
    'https://jsonplaceholder.typicode.com/posts/1',
    json=updated_post
)

print(f"Status Code: {response.status_code}")
result = response.json()
print(f"Updated Post:")
print(f"  Title: {result['title']}")
print(f"  Body: {result['body']}")

print("\n=== DELETE Request - Remove Data ===")

response = requests.delete('https://jsonplaceholder.typicode.com/posts/1')

print(f"Status Code: {response.status_code}")
print(f"Deletion successful: {response.status_code == 200}")

print("\n=== Custom Headers ===")

headers = {
    'User-Agent': 'Python Training Course/1.0',
    'Accept': 'application/json',
    'Custom-Header': 'Custom Value'
}

response = requests.get(
    'https://httpbin.org/headers',
    headers=headers
)

received_headers = response.json()
print("Headers sent and received:")
for key, value in received_headers['headers'].items():
    print(f"  {key}: {value}")

print("\n=== Error Handling ===")

try:
    response = requests.get('https://jsonplaceholder.typicode.com/users/999')
    response.raise_for_status()  # Raises exception for 4xx/5xx
    
    user = response.json()
    if not user:
        print("User not found (empty response)")
    else:
        print(f"Found: {user['name']}")
except requests.exceptions.HTTPError as e:
    print(f"HTTP Error: {e}")
except requests.exceptions.RequestException as e:
    print(f"Request Error: {e}")

print("\n=== Timeout and Retries ===")

try:
    response = requests.get(
        'https://jsonplaceholder.typicode.com/users/1',
        timeout=5  # 5 second timeout
    )
    print(f"Request completed in {response.elapsed.total_seconds():.3f}s")
except requests.exceptions.Timeout:
    print("Request timed out!")
```

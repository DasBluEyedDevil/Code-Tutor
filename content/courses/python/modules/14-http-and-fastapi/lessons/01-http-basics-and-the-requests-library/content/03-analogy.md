---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Basic request pattern:**
```python
import requests

# GET request
response = requests.get(url)

# Check status
if response.ok:  # or response.status_code == 200
    data = response.json()
```

**With parameters:**
```python
# Query parameters (?key=value&key2=value2)
params = {'key': 'value', 'key2': 'value2'}
response = requests.get(url, params=params)

# POST with JSON data
data = {'name': 'Alice', 'age': 25}
response = requests.post(url, json=data)

# Custom headers
headers = {'Authorization': 'Bearer token'}
response = requests.get(url, headers=headers)
```

**Error handling:**
```python
try:
    response = requests.get(url, timeout=5)
    response.raise_for_status()  # Raise exception for errors
    data = response.json()
except requests.exceptions.HTTPError:
    print("HTTP error")
except requests.exceptions.Timeout:
    print("Timeout")
except requests.exceptions.RequestException:
    print("Request failed")
```

**Common patterns:**
```python
# Check status
if response.status_code == 200:
    # Success
    pass

# Parse response
json_data = response.json()  # For JSON
text_data = response.text    # For text
binary = response.content    # For binary
```
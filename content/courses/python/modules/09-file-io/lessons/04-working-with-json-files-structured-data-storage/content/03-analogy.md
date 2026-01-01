---
type: "ANALOGY"
title: "Syntax Breakdown: JSON Operations"
---

**Import json module:**
```python
import json
```

**Python → JSON String:**
```python
data = {"name": "Alice", "age": 25}

# Convert to JSON string
json_string = json.dumps(data)
# '{"name": "Alice", "age": 25}'

# Pretty-printed (readable)
json_pretty = json.dumps(data, indent=2)
# {
#   "name": "Alice",
#   "age": 25
# }
```

**JSON String → Python:**
```python
json_string = '{"name": "Alice", "age": 25}'

# Convert to Python
data = json.loads(json_string)
# {'name': 'Alice', 'age': 25}
```

**Python → JSON File:**
```python
data = {"name": "Alice", "age": 25}

with open("data.json", "w") as file:
    json.dump(data, file, indent=2)
# File created with pretty JSON
```

**JSON File → Python:**
```python
with open("data.json", "r") as file:
    data = json.load(file)
# data is now a Python dict
```

**Type Conversions (JSON ↔ Python):**

| Python Type | JSON Type | Example |
|-------------|-----------|----------|
| dict | object | {"key": "value"} |
| list | array | [1, 2, 3] |
| str | string | "hello" |
| int, float | number | 42, 3.14 |
| True | true | true |
| False | false | false |
| None | null | null |

**Mnemonic: Remember 's' for string**
- `dump**s**` - dump to **s**tring
- `load**s**` - load from **s**tring
- `dump` - dump to **file**
- `load` - load from **file**

**Common options:**

```python
json.dumps(data, 
    indent=2,          # Pretty-print with 2 spaces
    sort_keys=True,    # Sort keys alphabetically
    ensure_ascii=False # Allow non-ASCII characters
)
```

**Error handling:**

```python
try:
    data = json.loads(json_string)
except json.JSONDecodeError as e:
    print(f"Invalid JSON: {e}")

try:
    with open("data.json", "r") as f:
        data = json.load(f)
except FileNotFoundError:
    print("File not found")
except json.JSONDecodeError:
    print("Invalid JSON in file")
```

**What CAN'T be converted to JSON:**

```python
# ❌ Sets
data = {1, 2, 3}  # Can't convert set

# ❌ Tuples (converted to arrays)
data = (1, 2, 3)  # Becomes [1, 2, 3] in JSON

# ❌ Functions
data = {"func": lambda x: x}  # Can't convert function

# ❌ Custom objects
class Person:
    pass
data = Person()  # Can't convert (need custom encoder)
```

**JSON can only handle:**
- Dictionaries
- Lists  
- Strings
- Numbers (int, float)
- Booleans (True/False)
- None (null)

**Complete workflow:**

```python
# 1. Create Python data
data = {"users": ["Alice", "Bob"], "count": 2}

# 2. Save to JSON file
with open("data.json", "w") as f:
    json.dump(data, f, indent=2)

# 3. Load from JSON file
with open("data.json", "r") as f:
    loaded = json.load(f)

# 4. Use the data
for user in loaded["users"]:
    print(user)
```
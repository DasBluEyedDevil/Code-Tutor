---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using json.dump vs json.dumps Incorrectly**
```python
# WRONG - dump() to string, dumps() to file
data = {"name": "Alice"}
json_string = json.dump(data)  # TypeError: missing file argument!
with open("data.json", "w") as f:
    f.write(json.dumps(data))  # Works but inefficient

# CORRECT - dump() to file, dumps() to string
json_string = json.dumps(data)  # Returns string
with open("data.json", "w") as f:
    json.dump(data, f)  # Writes directly to file
```

**2. Not Handling JSON Decode Errors**
```python
# WRONG - Crashes on invalid JSON
with open("data.json") as f:
    data = json.load(f)  # JSONDecodeError on invalid JSON!

# CORRECT - Handle parse errors
try:
    with open("data.json") as f:
        data = json.load(f)
except json.JSONDecodeError as e:
    print(f"Invalid JSON: {e}")
    data = {}
```

**3. Trying to Serialize Non-JSON Types**
```python
# WRONG - datetime not JSON serializable
import datetime
data = {"created": datetime.datetime.now()}
json.dumps(data)  # TypeError!

# CORRECT - Convert to string first
data = {"created": datetime.datetime.now().isoformat()}
json.dumps(data)  # Works
```

**4. Losing Data Types on Round-Trip**
```python
# WRONG - Tuples become lists, sets not supported
data = {"coords": (1, 2), "items": {1, 2, 3}}
json.dumps(data)  # TypeError for set!

# CORRECT - Convert to supported types
data = {"coords": [1, 2], "items": [1, 2, 3]}
# Or use custom encoder for complex types
```

**5. Not Using indent for Readable Output**
```python
# WRONG - Unreadable single-line JSON
data = {"users": [{"name": "Alice"}, {"name": "Bob"}]}
with open("data.json", "w") as f:
    json.dump(data, f)  # Hard to read/debug

# CORRECT - Use indent for pretty printing
with open("data.json", "w") as f:
    json.dump(data, f, indent=2)  # Human-readable
```
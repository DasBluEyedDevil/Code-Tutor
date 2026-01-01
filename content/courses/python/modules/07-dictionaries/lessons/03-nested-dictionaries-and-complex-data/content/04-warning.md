---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. KeyError When Accessing Missing Nested Keys**
```python
# WRONG - Any missing key in the chain causes KeyError
data = {"user": {"name": "Alice"}}
print(data["user"]["email"]["primary"])  # KeyError: 'email'

# CORRECT - Chain get() calls with empty dict defaults
print(data.get("user", {}).get("email", {}).get("primary", "N/A"))
# Returns "N/A" without crashing
```

**2. Accidentally Creating Shallow Copies**
```python
# WRONG - Assignment creates a reference, not a copy!
original = {"user": {"name": "Alice", "scores": [90, 85]}}
copy = original  # Same object!
copy["user"]["name"] = "Bob"
print(original["user"]["name"])  # "Bob" - original changed!

# CORRECT - Use copy.deepcopy() for nested structures
import copy
original = {"user": {"name": "Alice", "scores": [90, 85]}}
deep_copy = copy.deepcopy(original)
deep_copy["user"]["name"] = "Bob"
print(original["user"]["name"])  # "Alice" - original unchanged
```

**3. Forgetting Nested Key May Not Exist When Setting**
```python
# WRONG - Can't set nested key if parent doesn't exist
data = {}
data["level1"]["level2"] = "value"  # KeyError: 'level1'

# CORRECT - Create parent keys first
data = {}
data["level1"] = {}  # Create the parent dict first
data["level1"]["level2"] = "value"
# Or use setdefault()
data = {}
data.setdefault("level1", {})["level2"] = "value"
```

**4. Modifying Nested Dict While Iterating Over Parent**
```python
# WRONG - Deleting while iterating parent
users = {"alice": {"active": True}, "bob": {"active": False}}
for name, data in users.items():
    if not data["active"]:
        del users[name]  # RuntimeError!

# CORRECT - Collect keys first, then delete
to_remove = [n for n, d in users.items() if not d["active"]]
for name in to_remove:
    del users[name]
```

**5. Expecting .get() with {} Default to Be Reusable**
```python
# WRONG - Empty dict {} is created fresh each time (inefficient)
for _ in range(1000):
    value = data.get("missing", {}).get("nested")  # 1000 empty dicts!

# CORRECT - For repeated access, check once and store
inner = data.get("key", {})
if inner:  # Only access if inner exists
    value = inner.get("nested")
```
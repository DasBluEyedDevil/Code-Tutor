---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Modifying Dictionary While Iterating Over It**
```python
# WRONG - RuntimeError: dictionary changed size during iteration
scores = {"Alice": 95, "Bob": 60, "Charlie": 45}
for name, score in scores.items():
    if score < 70:
        del scores[name]  # Crashes!

# CORRECT - Create a list of keys to delete, then delete after
to_remove = [name for name, score in scores.items() if score < 70]
for name in to_remove:
    del scores[name]
# Or create a new dict with comprehension
scores = {name: score for name, score in scores.items() if score >= 70}
```

**2. Confusing pop() with del**
```python
# WRONG - Using pop() without storing the return value when you need it
data = {"key": "important_value"}
data.pop("key")  # Value is lost if you needed it!

# CORRECT - Store the popped value if you need it
value = data.pop("key")  # Now you have the value

# Also: pop() with missing key without default raises KeyError
data.pop("missing")  # KeyError!
data.pop("missing", None)  # Returns None, no error
```

**3. Expecting setdefault() to Update Existing Values**
```python
# WRONG - setdefault() doesn't change existing values
user = {"name": "Alice", "role": "user"}
user.setdefault("role", "admin")  # role stays "user", NOT "admin"

# CORRECT - Use direct assignment to update existing values
user["role"] = "admin"  # Now role is "admin"

# setdefault is for: set if missing, otherwise return existing
user.setdefault("age", 25)  # Sets age to 25 since it was missing
```

**4. Forgetting that keys() and values() Return Views, Not Lists**
```python
# WRONG - Treating view objects like lists
keys = {"a": 1, "b": 2}.keys()
keys[0]  # TypeError: 'dict_keys' object is not subscriptable

# CORRECT - Convert to list if you need indexing
keys = list({"a": 1, "b": 2}.keys())
print(keys[0])  # 'a'
```

**5. Using update() When You Want a New Dict**
```python
# WRONG - update() modifies the original dict in-place
original = {"a": 1}
extra = {"b": 2}
original.update(extra)
# original is now {'a': 1, 'b': 2} - modified!

# CORRECT - Use | operator for a new dict (Python 3.9+)
original = {"a": 1}
extra = {"b": 2}
combined = original | extra  # original unchanged
# Or use dict unpacking
combined = {**original, **extra}
```
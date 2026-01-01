---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. KeyError When Accessing Missing Keys**
```python
# WRONG - Crashes if key doesn't exist
user = {"name": "Alice"}
print(user["email"])  # KeyError: 'email'

# CORRECT - Use get() which returns None (or default) if missing
print(user.get("email"))           # None
print(user.get("email", "N/A"))    # "N/A"
```

**2. Confusing get() with [] for Assignment**
```python
# WRONG - get() doesn't add the key to the dictionary!
user = {"name": "Alice"}
user.get("age", 25)  # Returns 25 but doesn't add "age" to user
print(user)  # {'name': 'Alice'} - age is NOT there!

# CORRECT - Use [] to add/modify values
user["age"] = 25
print(user)  # {'name': 'Alice', 'age': 25}
```

**3. Using Mutable Objects (Lists) as Keys**
```python
# WRONG - Lists can't be dictionary keys!
my_dict = {}
my_dict[[1, 2, 3]] = "value"  # TypeError: unhashable type: 'list'

# CORRECT - Use tuples (immutable) instead
my_dict[(1, 2, 3)] = "value"  # Works!
my_dict["string_key"] = "value"  # Strings work too
```

**4. Forgetting to Check if Key Exists Before Accessing**
```python
# WRONG - Might crash
scores = {"Alice": 95, "Bob": 87}
if scores["Charlie"] > 90:  # KeyError if Charlie not in dict
    print("Great score!")

# CORRECT - Check first with 'in' or use get()
if "Charlie" in scores and scores["Charlie"] > 90:
    print("Great score!")
# Or use get() with a default that won't pass the condition
if scores.get("Charlie", 0) > 90:
    print("Great score!")
```

**5. Empty Dict vs None Check Confusion**
```python
# WRONG - Checking for None when dict might be empty
data = {}
if data is None:  # False! Empty dict is not None
    print("No data")

# CORRECT - Check if dict is empty (falsy)
if not data:  # True for empty dict
    print("No data")
# Or be explicit:
if len(data) == 0:
    print("No data")
```
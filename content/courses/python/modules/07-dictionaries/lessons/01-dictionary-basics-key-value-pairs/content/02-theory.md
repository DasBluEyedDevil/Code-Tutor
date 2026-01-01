---
type: "THEORY"
title: "Creating and Accessing Dictionaries"
---

**Creating a dictionary:**

```python
# Using curly braces (most common)
person = {"name": "Alice", "age": 30, "city": "NYC"}

# Empty dictionary
empty = {}

# Using dict() constructor
coords = dict(x=10, y=20)
```

**Accessing values:**

```python
person = {"name": "Alice", "age": 30}

# Using square brackets
print(person["name"])  # Alice

# Using get() - safer, returns None if key doesn't exist
print(person.get("name"))       # Alice
print(person.get("email"))      # None (no error!)
print(person.get("email", "N/A"))  # "N/A" (default value)
```

**Adding and modifying values:**

```python
person = {"name": "Alice"}

# Add new key-value pair
person["age"] = 30

# Modify existing value
person["name"] = "Alicia"

# Delete a key-value pair
del person["age"]
```

**Important:** Keys must be immutable (strings, numbers, tuples). Lists cannot be keys!
---
type: "THEORY"
title: "Understanding the Concept"
---

Dictionaries come with a powerful set of built-in methods that make working with key-value data easy and efficient. Think of these methods as tools in a toolbox - each one designed for a specific job.

**The most important dictionary methods:**

- **`.keys()`** - Get all the keys
- **`.values()`** - Get all the values
- **`.items()`** - Get all key-value pairs as tuples
- **`.get()`** - Safely get a value (with optional default)
- **`.update()`** - Merge another dictionary
- **`.pop()`** - Remove and return a value
- **`.clear()`** - Remove all items

**Looping through dictionaries:**

```python
scores = {"Alice": 95, "Bob": 87, "Charlie": 92}

# Loop through keys (default)
for name in scores:
    print(name)

# Loop through values
for score in scores.values():
    print(score)

# Loop through both (most common!)
for name, score in scores.items():
    print(f"{name}: {score}")
```

These methods make it easy to process, transform, and analyze dictionary data!
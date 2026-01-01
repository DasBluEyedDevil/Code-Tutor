---
type: "THEORY"
title: "Creating and Using Sets"
---

**Creating sets:**

```python
# Using curly braces (like dict, but no colons)
fruits = {"apple", "banana", "cherry"}

# Empty set (NOT {} - that's an empty dict!)
empty_set = set()

# From a list (removes duplicates)
numbers = set([1, 2, 2, 3, 3, 3])  # {1, 2, 3}

# From a string (each character becomes an item)
letters = set("hello")  # {'h', 'e', 'l', 'o'} - only 4 unique
```

**Adding and removing items:**

```python
fruits = {"apple", "banana"}

fruits.add("cherry")       # Add one item
fruits.update(["date", "elderberry"])  # Add multiple

fruits.remove("banana")    # Remove (raises error if missing)
fruits.discard("xyz")      # Remove (no error if missing)
item = fruits.pop()        # Remove and return arbitrary item
fruits.clear()             # Remove all items
```

**Membership testing:**

```python
colors = {"red", "green", "blue"}

print("red" in colors)     # True (very fast!)
print("yellow" in colors)  # False
print(len(colors))         # 3
```
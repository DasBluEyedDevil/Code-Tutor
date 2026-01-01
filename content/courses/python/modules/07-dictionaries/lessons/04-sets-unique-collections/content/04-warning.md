---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using {} for Empty Set (Creates Empty Dict Instead!)**
```python
# WRONG - This creates an empty DICTIONARY, not a set!
empty = {}
print(type(empty))  # <class 'dict'>

# CORRECT - Use set() for empty set
empty = set()
print(type(empty))  # <class 'set'>
```

**2. Using remove() on Missing Item Raises KeyError**
```python
# WRONG - remove() crashes if item not found
colors = {"red", "blue"}
colors.remove("green")  # KeyError: 'green'

# CORRECT - Use discard() which doesn't raise error
colors.discard("green")  # No error, just does nothing
# Or check first
if "green" in colors:
    colors.remove("green")
```

**3. Trying to Add Mutable Objects (Lists) to Sets**
```python
# WRONG - Lists can't be set items (unhashable)
my_set = set()
my_set.add([1, 2, 3])  # TypeError: unhashable type: 'list'

# CORRECT - Use tuples instead (immutable)
my_set.add((1, 2, 3))  # Works!
my_set.add("string")   # Strings work too
```

**4. Expecting Order to Be Preserved**
```python
# WRONG - Assuming sets maintain insertion order
numbers = {3, 1, 2}
print(list(numbers))  # Could be [1, 2, 3] or any order!

# CORRECT - Sort if order matters
numbers = {3, 1, 2}
print(sorted(numbers))  # [1, 2, 3] - guaranteed sorted
```

**5. Converting Set to List Loses Order of Original List**
```python
# WRONG - set() removes duplicates but also loses order
original = ["banana", "apple", "cherry", "apple"]
unique = list(set(original))  # Order is NOT preserved!

# CORRECT - Use dict.fromkeys() to preserve order (Python 3.7+)
original = ["banana", "apple", "cherry", "apple"]
unique = list(dict.fromkeys(original))
print(unique)  # ['banana', 'apple', 'cherry'] - order preserved!
```
---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. IndexError - Out of Range**
```python
# WRONG - Index 3 doesn't exist in 3-item list!
fruits = ["Apple", "Banana", "Cherry"]
print(fruits[3])  # IndexError: list index out of range

# CORRECT - Valid indices are 0, 1, 2
print(fruits[2])  # Cherry (last item)
# Or use -1 for last item:
print(fruits[-1])  # Cherry
```

**2. Off-By-One Error (1-Based Thinking)**
```python
# WRONG - Thinking index 1 is first item
fruits = ["Apple", "Banana", "Cherry"]
first = fruits[1]  # Gets Banana, not Apple!

# CORRECT - Python uses 0-based indexing
first = fruits[0]  # Gets Apple
```

**3. Using len() Directly as Index**
```python
# WRONG - len() is always out of range!
fruits = ["Apple", "Banana", "Cherry"]
last = fruits[len(fruits)]  # IndexError! len=3, but max index is 2

# CORRECT - Subtract 1 from length
last = fruits[len(fruits) - 1]  # Cherry
# Or just use -1:
last = fruits[-1]  # Cherry (simpler!)
```

**4. Negative Index Off-By-One**
```python
# WRONG - -0 is the same as 0, not last item
fruits = ["Apple", "Banana", "Cherry"]
last = fruits[-0]  # Gets Apple (same as [0])!

# CORRECT - Use -1 for last item
last = fruits[-1]  # Gets Cherry
```

**5. Empty List Access**
```python
# WRONG - Empty list has no valid indices!
empty = []
first = empty[0]  # IndexError: list index out of range

# CORRECT - Check length first
if len(empty) > 0:
    first = empty[0]
else:
    print("List is empty!")
```
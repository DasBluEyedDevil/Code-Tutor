---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting that stop index is EXCLUSIVE**
```python
# WRONG - expecting to include index 5
numbers = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
print(numbers[2:5])  # [2, 3, 4] - NOT [2, 3, 4, 5]!

# CORRECT - add 1 to include the desired end
print(numbers[2:6])  # [2, 3, 4, 5]
```

**2. Thinking slice modifies the original list**
```python
# WRONG - expecting original to change
original = [1, 2, 3, 4, 5]
my_slice = original[1:4]
my_slice[0] = 99
print(original)  # [1, 2, 3, 4, 5] - unchanged!

# CORRECT - use slice assignment to modify original
original = [1, 2, 3, 4, 5]
original[1:4] = [99, 99, 99]  # This DOES modify original
print(original)  # [1, 99, 99, 99, 5]
```

**3. Confusing slicing with indexing**
```python
# WRONG - expecting single item like indexing
numbers = [10, 20, 30, 40, 50]
result = numbers[2:3]  # Returns [30] (a list!)
print(result + 1)  # TypeError: can't add int to list

# CORRECT - indexing for single item, slice for range
single = numbers[2]   # 30 (integer)
sliced = numbers[2:3]  # [30] (list with one element)
```

**4. Wrong direction with negative step**
```python
# WRONG - can't go forward with negative step
numbers = [0, 1, 2, 3, 4, 5]
result = numbers[2:5:-1]  # [] empty list!

# CORRECT - start > stop when using negative step
result = numbers[5:2:-1]  # [5, 4, 3]
# Or use full reverse
result = numbers[::-1]  # [5, 4, 3, 2, 1, 0]
```

**5. Off-by-one errors in slice length calculations**
```python
# WRONG - calculating wrong slice for "middle 3"
data = [10, 20, 30, 40, 50, 60, 70]
middle = data[2:4]  # Only gets 2 items: [30, 40]

# CORRECT - length = stop - start
# For 3 items starting at index 2: 2 + 3 = 5
middle = data[2:5]  # [30, 40, 50] - correct 3 items
```
---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Off-By-One Error with range()**
```python
# WRONG - Prints 1-4, not 1-5!
for i in range(1, 5):
    print(i)  # Output: 1, 2, 3, 4

# CORRECT - Stop value is EXCLUDED
for i in range(1, 6):  # Goes UP TO but NOT INCLUDING 6
    print(i)  # Output: 1, 2, 3, 4, 5
```

**2. Modifying Loop Variable (Has No Effect!)**
```python
# WRONG - Changing i doesn't affect the loop!
for i in range(5):
    i = i + 100  # This is ignored!
    print(i)  # Prints 100, 101, 102, 103, 104
# i resets to next range value each iteration

# The loop ALWAYS iterates 5 times regardless
# If you need to skip iterations, use continue
```

**3. Using range() with Floats**
```python
# WRONG - range() only works with integers!
for i in range(0.5, 5.5, 0.5):  # TypeError!
    print(i)

# CORRECT - Use a while loop or numpy for floats
i = 0.5
while i < 5.5:
    print(i)
    i += 0.5
```

**4. Forgetting to Use enumerate()**
```python
# AWKWARD - Manually tracking index
fruits = ["apple", "banana", "cherry"]
index = 0
for fruit in fruits:
    print(f"{index}: {fruit}")
    index += 1

# PYTHONIC - Use enumerate()
for index, fruit in enumerate(fruits):
    print(f"{index}: {fruit}")
```

**5. Empty range() Produces No Iterations**
```python
# SILENT FAILURE - Loop never runs!
for i in range(5, 5):  # Start == stop
    print(i)  # Never executes!

for i in range(10, 5):  # Start > stop (no negative step)
    print(i)  # Never executes!

# To count backwards, use negative step:
for i in range(10, 5, -1):  # 10, 9, 8, 7, 6
    print(i)
```
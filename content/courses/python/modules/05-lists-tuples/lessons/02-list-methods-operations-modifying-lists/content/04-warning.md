---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Assigning the return value of modifying methods**
```python
# WRONG - append() returns None, not the list!
my_list = [1, 2, 3]
my_list = my_list.append(4)  # my_list is now None!
print(my_list)  # None

# CORRECT - modifying methods change the list in place
my_list = [1, 2, 3]
my_list.append(4)  # Modifies my_list directly
print(my_list)  # [1, 2, 3, 4]
```

**2. Calling remove() on an item not in the list**
```python
# WRONG - raises ValueError if item doesn't exist
numbers = [1, 2, 3]
numbers.remove(5)  # ValueError: list.remove(x): x not in list

# CORRECT - check membership first
numbers = [1, 2, 3]
if 5 in numbers:
    numbers.remove(5)
else:
    print("Item not found")
```

**3. Confusing append() with extend()**
```python
# WRONG - append() adds the list as a single element
fruits = ["apple"]
fruits.append(["banana", "cherry"])
print(fruits)  # ["apple", ["banana", "cherry"]] - nested list!

# CORRECT - use extend() to add multiple items
fruits = ["apple"]
fruits.extend(["banana", "cherry"])
print(fruits)  # ["apple", "banana", "cherry"]
```

**4. Forgetting that sort() modifies the original list**
```python
# WRONG - assuming sort() returns a new list
original = [3, 1, 2]
sorted_list = original.sort()  # sorted_list is None!
print(original)  # [1, 2, 3] - original was modified!

# CORRECT - use sorted() to get a new list
original = [3, 1, 2]
sorted_list = sorted(original)  # Returns new list
print(original)  # [3, 1, 2] - unchanged
print(sorted_list)  # [1, 2, 3]
```

**5. Using index() without checking if item exists**
```python
# WRONG - raises ValueError if item not found
fruits = ["apple", "banana"]
pos = fruits.index("cherry")  # ValueError!

# CORRECT - check membership first
fruits = ["apple", "banana"]
if "cherry" in fruits:
    pos = fruits.index("cherry")
else:
    pos = -1  # Convention for "not found"
```
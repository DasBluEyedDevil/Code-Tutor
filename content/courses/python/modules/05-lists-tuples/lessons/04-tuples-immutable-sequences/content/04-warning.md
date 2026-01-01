---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting the comma in single-item tuples**
```python
# WRONG - parentheses alone don't make a tuple
single = (42)  # This is just an integer!
print(type(single))  # <class 'int'>

# CORRECT - comma is required for single-item tuple
single = (42,)  # Now it's a tuple
print(type(single))  # <class 'tuple'>
```

**2. Trying to modify a tuple**
```python
# WRONG - tuples are immutable
coords = (10, 20)
coords[0] = 15  # TypeError: tuple doesn't support item assignment

# CORRECT - create a new tuple
coords = (10, 20)
coords = (15,) + coords[1:]  # Creates new tuple (15, 20)
# Or convert to list, modify, convert back
temp = list(coords)
temp[0] = 15
coords = tuple(temp)
```

**3. Using list methods on tuples**
```python
# WRONG - tuples don't have list methods
my_tuple = (1, 2, 3)
my_tuple.append(4)  # AttributeError!
my_tuple.sort()     # AttributeError!

# CORRECT - tuples only have count() and index()
count = my_tuple.count(2)  # 1
pos = my_tuple.index(3)    # 2
# For sorted version, use sorted()
sorted_list = sorted(my_tuple)  # Returns a list
```

**4. Unpacking wrong number of values**
```python
# WRONG - number of variables must match tuple length
point = (10, 20, 30)
x, y = point  # ValueError: too many values to unpack

# CORRECT - match the number of values
x, y, z = point  # Works: x=10, y=20, z=30
# Or use * to collect extras
x, *rest = point  # x=10, rest=[20, 30]
```

**5. Expecting tuples to work like lists**
```python
# WRONG - can't use tuples as dictionary keys if they contain mutable items
bad_key = ([1, 2], "data")  # Contains a list
my_dict = {bad_key: "value"}  # TypeError: unhashable type: 'list'

# CORRECT - tuple with only immutable items
good_key = ((1, 2), "data")  # Tuple of tuples
my_dict = {good_key: "value"}  # Works!
```
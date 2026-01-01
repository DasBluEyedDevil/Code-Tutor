---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Confusing Conditional Position in Comprehensions**
```python
# WRONG - Filter condition needs 'else' at the start
result = [x if x > 5 for x in range(10)]  # SyntaxError!

# CORRECT - Filter at end (no else)
result = [x for x in range(10) if x > 5]  # [6, 7, 8, 9]

# CORRECT - Transform at start (needs else)
result = [x if x > 5 else 0 for x in range(10)]
```

**2. Creating Empty Dict Instead of Set**
```python
# WRONG - Empty braces create dict, not set
empty = {}  # This is a dict!
print(type(empty))  # <class 'dict'>

# CORRECT - Use set() for empty set
empty = set()  # This is a set
```

**3. Overwriting Dict Keys in Comprehension**
```python
# WRONG - Duplicate keys overwrite silently
items = [('a', 1), ('b', 2), ('a', 3)]
result = {k: v for k, v in items}  # {'a': 3} - First lost!

# CORRECT - Handle duplicates with defaultdict
from collections import defaultdict
result = defaultdict(list)
```

**4. Nested Comprehension Read Order**
```python
# WRONG - Thinking inner loop comes first
matrix = [[1, 2], [3, 4]]
result = [x for row in matrix for x in row]  # [1,2,3,4] flat!

# CORRECT - Use nested comprehensions for structure
result = [[x * 2 for x in row] for row in matrix]
```

**5. Using List Comprehension for Side Effects**
```python
# WRONG - Creating list for side effects
[print(x) for x in range(5)]  # Wasteful

# CORRECT - Use regular loop
for x in range(5):
    print(x)
```
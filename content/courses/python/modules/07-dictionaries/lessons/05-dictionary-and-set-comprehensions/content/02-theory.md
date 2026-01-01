---
type: "THEORY"
title: "Advanced Comprehension Patterns"
---

**Adding conditions (filtering):**

```python
# Only even numbers
evens = {x: x ** 2 for x in range(10) if x % 2 == 0}
# {0: 0, 2: 4, 4: 16, 6: 36, 8: 64}

# Words longer than 3 characters
long_words = {word.upper() for word in words if len(word) > 3}
```

**Transforming data:**

```python
# Swap keys and values
original = {"a": 1, "b": 2, "c": 3}
swapped = {v: k for k, v in original.items()}
# {1: 'a', 2: 'b', 3: 'c'}

# Create lookup from list
names = ["Alice", "Bob", "Charlie"]
name_lengths = {name: len(name) for name in names}
# {'Alice': 5, 'Bob': 3, 'Charlie': 7}
```

**From two lists to dictionary:**

```python
keys = ["name", "age", "city"]
values = ["Alice", 30, "NYC"]

# Using zip() with comprehension
person = {k: v for k, v in zip(keys, values)}
# {'name': 'Alice', 'age': 30, 'city': 'NYC'}

# Simpler: just use dict()
person = dict(zip(keys, values))  # Same result!
```

**Nested comprehensions (use sparingly!):**

```python
# Multiplication table
table = {i: {j: i * j for j in range(1, 4)} for i in range(1, 4)}
# {1: {1: 1, 2: 2, 3: 3}, 2: {1: 2, 2: 4, 3: 6}, ...}
```
---
type: "THEORY"
title: "Built-in Functions You Should Know"
---

Python comes with many powerful built-in functions. Here are the most useful ones:

**Working with Numbers:**
- `abs(x)` - Absolute value: `abs(-5)` returns `5`
- `round(x, n)` - Round to n decimal places: `round(3.14159, 2)` returns `3.14`
- `min(...)` - Smallest value: `min(3, 1, 4)` returns `1`
- `max(...)` - Largest value: `max(3, 1, 4)` returns `4`
- `sum(iterable)` - Sum all values: `sum([1, 2, 3])` returns `6`

**Working with Collections:**
- `len(x)` - Length/count: `len([1, 2, 3])` returns `3`
- `sorted(x)` - Return sorted list: `sorted([3, 1, 2])` returns `[1, 2, 3]`
- `reversed(x)` - Return reversed iterator
- `enumerate(x)` - Get index and value pairs
- `zip(a, b)` - Pair up elements from multiple lists

**Type Conversions:**
- `int(x)` - Convert to integer: `int("42")` returns `42`
- `float(x)` - Convert to float: `float("3.14")` returns `3.14`
- `str(x)` - Convert to string: `str(42)` returns `"42"`
- `list(x)` - Convert to list: `list("abc")` returns `['a', 'b', 'c']`
- `bool(x)` - Convert to boolean: `bool(0)` returns `False`

**Higher-Order Functions (use with lambdas!):**
- `map(func, iterable)` - Apply function to each item
- `filter(func, iterable)` - Keep items where function returns True
- `any(iterable)` - True if ANY item is truthy
- `all(iterable)` - True if ALL items are truthy
---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**List comprehension patterns:**

```python
# 1. Basic transformation
[x*2 for x in numbers]

# 2. Filter only
[x for x in numbers if x > 0]

# 3. Transform with filter
[x*2 for x in numbers if x > 0]

# 4. Conditional transform (no filter)
[x if x > 0 else 0 for x in numbers]

# 5. Conditional transform AND filter
[x*2 if x > 10 else x for x in numbers if x != 0]
```

**Dictionary comprehension:**
```python
{key: value for item in iterable}
{x: x**2 for x in range(5)}
# {0: 0, 1: 1, 2: 4, 3: 9, 4: 16}
```

**Set comprehension:**
```python
{expression for item in iterable}
{len(word) for word in words}
# {3, 5, 6}  # Unique lengths
```

**When NOT to use:**
```python
# Bad - too complex
[process(x, y) for x in data if validate(x) 
 for y in x.items if y.type == 'special' 
 if check(y)]

# Better - use regular loop
results = []
for x in data:
    if validate(x):
        for y in x.items:
            if y.type == 'special' and check(y):
                results.append(process(x, y))
```
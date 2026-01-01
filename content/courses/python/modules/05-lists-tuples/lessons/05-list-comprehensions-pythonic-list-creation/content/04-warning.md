---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Confusing filter if vs conditional expression if-else**
```python
# WRONG - using else with filter (at the end)
result = [x for x in numbers if x > 0 else 0]  # SyntaxError!

# CORRECT - filter (at end, no else allowed)
positive = [x for x in numbers if x > 0]  # Keep only positive

# CORRECT - conditional expression (before for, must have else)
result = [x if x > 0 else 0 for x in numbers]  # Replace negatives with 0
```

**2. Using comprehension for side effects**
```python
# WRONG - creating unnecessary list for side effects
[print(x) for x in items]  # Creates list of None values

# CORRECT - use regular loop for side effects
for x in items:
    print(x)
```

**3. Too complex comprehension (hard to read)**
```python
# WRONG - too complex, hard to understand
result = [x**2 if x > 0 else x**3 if x < 0 else 0 for x in data if abs(x) > 5 and x % 2 == 0]

# CORRECT - use regular loop for complex logic
result = []
for x in data:
    if abs(x) > 5 and x % 2 == 0:
        if x > 0:
            result.append(x**2)
        elif x < 0:
            result.append(x**3)
        else:
            result.append(0)
```

**4. Variable scope confusion**
```python
# CAUTION - loop variable exists after comprehension
[x * 2 for x in range(5)]
print(x)  # 4 - x still exists! (Python 3)

# BETTER - use descriptive names to avoid confusion
[item * 2 for item in range(5)]
# Now 'item' is less likely to conflict with other variables
```

**5. Forgetting to handle empty iterables**
```python
# WRONG - assuming list has items
scores = []
average = sum([s for s in scores]) / len(scores)  # ZeroDivisionError!

# CORRECT - check for empty
scores = []
if scores:
    average = sum(scores) / len(scores)
else:
    average = 0
```
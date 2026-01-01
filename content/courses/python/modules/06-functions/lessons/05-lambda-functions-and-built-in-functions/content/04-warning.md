---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Lambda Capturing Loop Variable by Reference (Late Binding)**
```python
# WRONG - All lambdas capture the FINAL value of i
functions = []
for i in range(3):
    functions.append(lambda: i)

print([f() for f in functions])  # [2, 2, 2] - Not [0, 1, 2]!

# CORRECT - Capture value with default argument
functions = []
for i in range(3):
    functions.append(lambda i=i: i)

print([f() for f in functions])  # [0, 1, 2]
```

**2. Using Lambda for Complex Logic**
```python
# WRONG - Hard to read and debug
process = lambda x: x * 2 if x > 0 else (x * -1 if x < -10 else 0)

# CORRECT - Use a regular function for complex logic
def process(x):
    if x > 0:
        return x * 2
    elif x < -10:
        return x * -1
    else:
        return 0
```

**3. Forgetting That map() and filter() Return Iterators**
```python
# WRONG - Iterator exhausted after first use
numbers = [1, 2, 3, 4, 5]
doubled = map(lambda x: x * 2, numbers)

print(list(doubled))  # [2, 4, 6, 8, 10]
print(list(doubled))  # [] - Empty! Iterator exhausted!

# CORRECT - Convert to list if you need to reuse
doubled = list(map(lambda x: x * 2, numbers))
print(doubled)  # [2, 4, 6, 8, 10]
print(doubled)  # [2, 4, 6, 8, 10] - Works!
```

**4. Using Lambda When a Built-in Function Exists**
```python
# WRONG - Reinventing the wheel
numbers = [1, 2, 3, 4, 5]
total = sum(map(lambda x: x, numbers))  # Unnecessary lambda!

# CORRECT - Just use sum() directly
total = sum(numbers)

# WRONG - Lambda just calls a function
sorted_words = sorted(words, key=lambda x: len(x))

# CORRECT - Pass the function directly
sorted_words = sorted(words, key=len)
```

**5. Confusing Return Behavior in Lambda vs Regular Functions**
```python
# WRONG - Using return keyword in lambda
square = lambda x: return x ** 2  # SyntaxError!

# CORRECT - Lambda automatically returns the expression
square = lambda x: x ** 2

# WRONG - Expecting None from expression lambda
do_print = lambda x: print(x)  # Returns None (print's return value)
result = do_print(5)  # result is None

# CORRECT - If you need side effects, use a regular function
def do_print(x):
    print(x)
    return x  # Now returns the value
```
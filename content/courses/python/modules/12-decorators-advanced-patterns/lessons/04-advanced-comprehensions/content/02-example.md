---
type: "EXAMPLE"
title: "Code Example: List Comprehensions"
---

**List comprehension syntax:**

**Basic:**
```python
[expression for item in iterable]
```

**With filter:**
```python
[expression for item in iterable if condition]
```

**With conditional expression:**
```python
[expr1 if condition else expr2 for item in iterable]
```

**Nested:**
```python
[expression for outer in iterable1 for inner in iterable2]
# Same as:
for outer in iterable1:
    for inner in iterable2:
        expression
```

**Key difference:**
- Filter: `if` at end (no else)
- Transform: `if/else` before `for`

```python
print("=== Basic List Comprehension ===")

# Traditional loop
squares_loop = []
for x in range(10):
    squares_loop.append(x**2)
print(f"Loop: {squares_loop}")

# List comprehension
squares_comp = [x**2 for x in range(10)]
print(f"Comprehension: {squares_comp}")

print("\n=== Comprehension with Condition (Filter) ===")

# Only even numbers
evens = [x for x in range(20) if x % 2 == 0]
print(f"Evens: {evens}")

# Only positive numbers
numbers = [-5, -3, -1, 0, 2, 4, 6]
positive = [x for x in numbers if x > 0]
print(f"Positive: {positive}")

# Words longer than 3 characters
words = ['a', 'the', 'cat', 'in', 'hat', 'python', 'is', 'cool']
long_words = [word for word in words if len(word) > 3]
print(f"Long words: {long_words}")

print("\n=== Comprehension with Transformation ===")

# Uppercase all words
upper_words = [word.upper() for word in words]
print(f"Uppercase: {upper_words}")

# Length of each word
word_lengths = [len(word) for word in words]
print(f"Lengths: {word_lengths}")

# Complex transformation
users = ['alice', 'bob', 'charlie']
user_info = [f"User: {name.title()} ({len(name)} chars)" for name in users]
print(f"User info: {user_info}")

print("\n=== Comprehension with if-else (Transform) ===")

# Classify numbers as even or odd
numbers = [1, 2, 3, 4, 5, 6]
classified = ['even' if x % 2 == 0 else 'odd' for x in numbers]
print(f"Classified: {classified}")

# Cap values at 100
scores = [85, 92, 105, 78, 110, 88]
capped = [score if score <= 100 else 100 for score in scores]
print(f"Capped scores: {capped}")

# Absolute values using conditional
values = [-5, 3, -2, 8, -1]
absolute = [x if x >= 0 else -x for x in values]
print(f"Absolute: {absolute}")

print("\n=== Multiple Conditions ===")

# Filter: divisible by 2 AND divisible by 3
numbers = range(1, 31)
div_by_6 = [x for x in numbers if x % 2 == 0 if x % 3 == 0]
print(f"Divisible by 6: {div_by_6}")

# Same as: if x % 2 == 0 and x % 3 == 0
div_by_6_alt = [x for x in numbers if x % 2 == 0 and x % 3 == 0]
print(f"Alternative: {div_by_6_alt}")

print("\n=== Nested Loops in Comprehension ===")

# Cartesian product
colors = ['red', 'blue']
sizes = ['S', 'M', 'L']
combinations = [f"{color}-{size}" for color in colors for size in sizes]
print(f"Combinations: {combinations}")

# Flatten nested list
nested = [[1, 2], [3, 4], [5, 6]]
flattened = [num for sublist in nested for num in sublist]
print(f"Flattened: {flattened}")

# Matrix operations
matrix = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
]
# Get all elements
all_elements = [num for row in matrix for num in row]
print(f"All elements: {all_elements}")

# Transpose matrix
transposed = [[row[i] for row in matrix] for i in range(3)]
print(f"Transposed: {transposed}")
```

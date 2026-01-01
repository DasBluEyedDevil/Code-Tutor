---
type: "THEORY"
title: "Syntax Breakdown"
---

### List Comprehension Syntax:
```
# Basic form:
[expression for item in iterable]

# With filter:
[expression for item in iterable if condition]

# With if-else (conditional expression):
[expression_if_true if condition else expression_if_false for item in iterable]

```
### Reading List Comprehensions:
Read from LEFT to RIGHT (like English):

```
[x * 2 for x in numbers if x > 0]
 ↓      ↓         ↓         ↓
 "x*2"  "for x"   "in       "if x > 0"
                  numbers"

Reads as: "Create x*2 for each x in numbers if x > 0"

```
### Form 1: Basic Transformation
```
# Syntax:
[expression for item in iterable]

# Examples:
[x * 2 for x in numbers]              # Double each
[word.upper() for word in words]      # Uppercase each
[x**2 for x in range(10)]             # Square 0-9
[len(word) for word in words]         # Length of each

```
### Form 2: With Filter (if)
```
# Syntax:
[expression for item in iterable if condition]

# Examples:
[x for x in numbers if x > 0]         # Positive only
[x * 2 for x in numbers if x % 2 == 0] # Double evens
[word for word in words if len(word) > 5] # Long words
[x**2 for x in range(20) if x % 3 == 0] # Squares of multiples of 3

```
**Note:** The if at the END is a filter (removes items). No else allowed here!

### Form 3: Conditional Expression (if-else)
```
# Syntax:
[expr_if_true if condition else expr_if_false for item in iterable]

# Examples:
["even" if x % 2 == 0 else "odd" for x in numbers]
[x if x > 0 else 0 for x in numbers]  # Clip negatives to 0
[word.upper() if len(word) > 5 else word.lower() for word in words]

```
**Note:** The if-else is BEFORE the for - it's a conditional expression, not a filter!

### Syntax Comparison:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Pattern</th><th>Syntax</th><th>Purpose</th></tr><tr><td>Filter</td><td>[x for x in nums if x > 0]</td><td>Keep some items</td></tr><tr><td>Transform</td><td>[x*2 for x in nums]</td><td>Change all items</td></tr><tr><td>Transform + Filter</td><td>[x*2 for x in nums if x > 0]</td><td>Change and keep some</td></tr><tr><td>Conditional</td><td>["Y" if x > 0 else "N" for x in nums]</td><td>Different transforms</td></tr></table>### Equivalent Loop Code:
```
# List comprehension:
result = [x * 2 for x in numbers if x > 0]

# Equivalent loop:
result = []
for x in numbers:
    if x > 0:
        result.append(x * 2)

# Conditional expression:
result = ["even" if x % 2 == 0 else "odd" for x in numbers]

# Equivalent loop:
result = []
for x in numbers:
    if x % 2 == 0:
        result.append("even")
    else:
        result.append("odd")

```
### Nested List Comprehensions:
```
# Flatten 2D list:
matrix = [[1, 2, 3], [4, 5, 6]]
flat = [num for row in matrix for num in row]
# [1, 2, 3, 4, 5, 6]

# Reads as:
# "For each row in matrix, for each num in row, take num"

# Equivalent loop:
flat = []
for row in matrix:
    for num in row:
        flat.append(num)

# Create 2D list:
table = [[row * col for col in range(1, 4)] for row in range(1, 4)]
# [[1, 2, 3], [2, 4, 6], [3, 6, 9]]

```
### Common Patterns:
```
# Squares of 0-9
squares = [x**2 for x in range(10)]

# Even numbers 0-20
evens = [x for x in range(21) if x % 2 == 0]

# First letters
first_letters = [word[0] for word in words]

# Lengths
lengths = [len(item) for item in items]

# Uppercase all
upper = [s.upper() for s in strings]

# Strip whitespace
cleaned = [s.strip() for s in strings]

# Absolute values
absolute = [x if x >= 0 else -x for x in numbers]

# Clamp to range
clamped = [x if x <= 100 else 100 for x in scores]

# Extract dict values
names = [person["name"] for person in people]

# Generate test data
emails = [f"user{i}@test.com" for i in range(100)]

```
### Common Mistakes:
#### 1. Confusing Filter vs Conditional Expression
```
# WRONG: Trying to use else with filter
[x for x in numbers if x > 0 else 0]  # SyntaxError!

# CORRECT: Use conditional expression
[x if x > 0 else 0 for x in numbers]

# Or use filter only:
[x for x in numbers if x > 0]

```
#### 2. Too Complex (Hard to Read)
```
# TOO COMPLEX:
result = [x**2 if x > 0 else x**3 if x < 0 else 0 for x in numbers if abs(x) > 5]

# BETTER: Use regular loop for clarity
result = []
for x in numbers:
    if abs(x) > 5:
        if x > 0:
            result.append(x**2)
        elif x < 0:
            result.append(x**3)
        else:
            result.append(0)

```
#### 3. Side Effects (Don't Use Comprehensions)
```
# WRONG: Using comprehension for side effects
[print(x) for x in numbers]  # Creates wasteful list!

# CORRECT: Use regular loop
for x in numbers:
    print(x)

```
### When to Use List Comprehensions:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Use When:</th><th>Don't Use When:</th></tr><tr><td>Simple transformation</td><td>Complex logic (>2 conditions)</td></tr><tr><td>Simple filter</td><td>Multiple statements needed</td></tr><tr><td>Creating new list</td><td>Side effects (print, write)</td></tr><tr><td>One line is clear</td><td>Readability suffers</td></tr><tr><td>Performance matters</td><td>Debugging needed</td></tr></table>
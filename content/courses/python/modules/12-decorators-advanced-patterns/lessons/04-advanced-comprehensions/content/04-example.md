---
type: "EXAMPLE"
title: "Code Example: Dict and Set Comprehensions"
---

**Dictionary comprehension syntax:**
```python
{key_expr: value_expr for item in iterable}
{key_expr: value_expr for item in iterable if condition}
```

**Set comprehension syntax:**
```python
{expression for item in iterable}
{expression for item in iterable if condition}
```

**Practical patterns:**

**1. Create lookup tables:**
```python
lookup = {item.id: item for item in items}
```

**2. Invert dictionaries:**
```python
inverted = {v: k for k, v in orig.items()}
```

**3. Filter dictionaries:**
```python
filtered = {k: v for k, v in d.items() if condition}
```

**4. Get unique values:**
```python
unique = {item.property for item in items}
```

```python
print("=== Dictionary Comprehension ===")

# Create dict from range
squares_dict = {x: x**2 for x in range(6)}
print(f"Squares: {squares_dict}")

# From two lists
names = ['Alice', 'Bob', 'Charlie']
ages = [25, 30, 35]
people = {name: age for name, age in zip(names, ages)}
print(f"People: {people}")

# Swap keys and values
original = {'a': 1, 'b': 2, 'c': 3}
swapped = {value: key for key, value in original.items()}
print(f"Swapped: {swapped}")

# Filter dictionary
scores = {'Alice': 95, 'Bob': 67, 'Charlie': 89, 'David': 45}
passing = {name: score for name, score in scores.items() if score >= 70}
print(f"Passing: {passing}")

# Transform values
temps_celsius = {'morning': 20, 'noon': 28, 'evening': 22}
temps_fahrenheit = {time: (temp * 9/5) + 32 
                    for time, temp in temps_celsius.items()}
print(f"Fahrenheit: {temps_fahrenheit}")

print("\n=== Set Comprehension ===")

# Unique squares
numbers = [1, 2, 3, 4, 5, 1, 2, 3]
unique_squares = {x**2 for x in numbers}
print(f"Unique squares: {unique_squares}")

# Unique word lengths
sentence = "the quick brown fox jumps over the lazy dog"
word_lengths = {len(word) for word in sentence.split()}
print(f"Unique word lengths: {sorted(word_lengths)}")

# Unique first letters
words = ['apple', 'banana', 'apricot', 'blueberry', 'cherry']
first_letters = {word[0] for word in words}
print(f"First letters: {sorted(first_letters)}")

print("\n=== Practical Examples ===")

# Count word frequency
text = "python is great and python is fun and python is powerful"
words = text.split()
word_count = {word: words.count(word) for word in set(words)}
print(f"Word frequency: {word_count}")

# Group by property
students = [
    {'name': 'Alice', 'grade': 'A'},
    {'name': 'Bob', 'grade': 'B'},
    {'name': 'Charlie', 'grade': 'A'},
    {'name': 'David', 'grade': 'C'},
    {'name': 'Eve', 'grade': 'B'}
]

# Group names by grade
by_grade = {}
for student in students:
    grade = student['grade']
    if grade not in by_grade:
        by_grade[grade] = []
    by_grade[grade].append(student['name'])

print(f"\nGrouped by grade: {by_grade}")

# Create lookup table
products = [
    {'id': 1, 'name': 'Widget', 'price': 9.99},
    {'id': 2, 'name': 'Gadget', 'price': 19.99},
    {'id': 3, 'name': 'Doohickey', 'price': 14.99}
]

product_lookup = {p['id']: p for p in products}
print(f"\nProduct lookup:")
for pid, product in product_lookup.items():
    print(f"  {pid}: {product['name']} - ${product['price']}")

# Multi-level filtering and transformation
data = [
    {'name': 'file1.py', 'size': 1024},
    {'name': 'file2.txt', 'size': 2048},
    {'name': 'file3.py', 'size': 512},
    {'name': 'file4.txt', 'size': 128}
]

# Python files with size in KB
py_files = {f['name']: f['size'] / 1024 
            for f in data 
            if f['name'].endswith('.py')}
print(f"\nPython files (KB): {py_files}")

print("\n=== Combining Comprehensions ===")

# Matrix to dict of dicts
matrix = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
]

# Create dict: {row_index: {col_index: value}}
matrix_dict = {i: {j: val for j, val in enumerate(row)} 
               for i, row in enumerate(matrix)}
print(f"Matrix as dict:")
for row_idx, row_dict in matrix_dict.items():
    print(f"  Row {row_idx}: {row_dict}")

# Nested dict comprehension: grade statistics
grades = {
    'Math': [85, 90, 78, 92],
    'Science': [88, 76, 95, 84],
    'English': [92, 89, 91, 88]
}

stats = {
    subject: {
        'avg': sum(scores) / len(scores),
        'max': max(scores),
        'min': min(scores)
    }
    for subject, scores in grades.items()
}

print(f"\nGrade statistics:")
for subject, stat in stats.items():
    print(f"  {subject}: avg={stat['avg']:.1f}, max={stat['max']}, min={stat['min']}")
```

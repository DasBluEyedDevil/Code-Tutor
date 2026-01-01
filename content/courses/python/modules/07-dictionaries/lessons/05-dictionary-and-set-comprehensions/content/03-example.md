---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Basic Dictionary Comprehension ===
Squares: {1: 1, 2: 4, 3: 9, 4: 16, 5: 25}
Word lengths: {'apple': 5, 'banana': 6, 'cherry': 6}

=== Basic Set Comprehension ===
Unique lengths: {5, 6}
Even squares: {0, 64, 4, 36, 16}

=== With Conditions ===
Passing students: {'Alice': 85, 'Charlie': 78}
Long words uppercase: {'BANANA', 'CHERRY', 'APPLE'}

=== Transforming Data ===
Swapped: {1: 'a', 2: 'b', 3: 'c'}
Uppercase keys: {'ALICE': 85, 'BOB': 62, 'CHARLIE': 78}

=== Practical: From Lists to Dict ===
Person: {'name': 'Alice', 'age': 30, 'city': 'NYC'}

=== Practical: Word Frequency ===
Word counts: {'hello': 2, 'world': 1, 'python': 2}
```

```python
# Dictionary and Set Comprehensions

print("=== Basic Dictionary Comprehension ===")

# Create a dictionary of squares
squares = {x: x ** 2 for x in range(1, 6)}
print(f"Squares: {squares}")

# Create a dictionary of word lengths
words = ["apple", "banana", "cherry"]
word_lengths = {word: len(word) for word in words}
print(f"Word lengths: {word_lengths}")

print("\n=== Basic Set Comprehension ===")

# Unique word lengths
words = ["apple", "banana", "cherry"]
unique_lengths = {len(word) for word in words}
print(f"Unique lengths: {unique_lengths}")

# Set of even squares
even_squares = {x ** 2 for x in range(10) if x % 2 == 0}
print(f"Even squares: {even_squares}")

print("\n=== With Conditions ===")

# Only passing grades (75+)
scores = {"Alice": 85, "Bob": 62, "Charlie": 78}
passing = {name: score for name, score in scores.items() if score >= 75}
print(f"Passing students: {passing}")

# Long words only, converted to uppercase
words = ["hi", "apple", "banana", "ok", "cherry"]
long_upper = {word.upper() for word in words if len(word) > 3}
print(f"Long words uppercase: {long_upper}")

print("\n=== Transforming Data ===")

# Swap keys and values
original = {"a": 1, "b": 2, "c": 3}
swapped = {v: k for k, v in original.items()}
print(f"Swapped: {swapped}")

# Transform keys to uppercase
grades = {"Alice": 85, "Bob": 62, "Charlie": 78}
upper_grades = {name.upper(): score for name, score in grades.items()}
print(f"Uppercase keys: {upper_grades}")

print("\n=== Practical: From Lists to Dict ===")

keys = ["name", "age", "city"]
values = ["Alice", 30, "NYC"]

# Using comprehension with zip
person = {k: v for k, v in zip(keys, values)}
print(f"Person: {person}")

print("\n=== Practical: Word Frequency ===")

text = "hello world hello python world python hello"
words = text.split()

# Count word occurrences
word_counts = {word: words.count(word) for word in set(words)}
print(f"Word counts: {word_counts}")
```

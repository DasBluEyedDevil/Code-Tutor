---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
============================================================
LIST COMPREHENSIONS
============================================================

=== Basic Transformations ===

Original numbers: [1, 2, 3, 4, 5]

Traditional approach (loop + append):
  Code: for num in numbers: doubled.append(num * 2)
  Result: [2, 4, 6, 8, 10]

List comprehension:
  Code: [num * 2 for num in numbers]
  Result: [2, 4, 6, 8, 10]

Squares 1-10: [1, 4, 9, 16, 25, 36, 49, 64, 81, 100]
Cubes 1-5: [1, 8, 27, 64, 125]

=== String Operations ===

Original words: ['python', 'java', 'javascript', 'ruby', 'go']

Uppercase: ['PYTHON', 'JAVA', 'JAVASCRIPT', 'RUBY', 'GO']
Capitalized: ['Python', 'Java', 'Javascript', 'Ruby', 'Go']
First letters: ['p', 'j', 'j', 'r', 'g']
Lengths: [6, 4, 10, 4, 2]
Excited: ['python!', 'java!', 'javascript!', 'ruby!', 'go!']

=== Filtering with if ===

Numbers: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

Even numbers: [2, 4, 6, 8, 10]
Odd numbers: [1, 3, 5, 7, 9]
Greater than 5: [6, 7, 8, 9, 10]
Divisible by 3: [3, 6, 9]

Words: ['apple', 'banana', 'cat', 'dog', 'elephant', 'fig']
Long words (>5): ['banana', 'elephant']
Start with vowel: ['apple', 'elephant']

=== Transform + Filter ===

Numbers: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

Squares of evens: [4, 16, 36, 64, 100]
Doubled odds: [2, 6, 10, 14, 18]

Words: ['Python', 'Java', 'C', 'JavaScript', 'Go', 'Ruby']
Uppercase long: ['PYTHON', 'JAVASCRIPT']
Lowercase short: ['java', 'c', 'go', 'ruby']

=== Conditional Expressions (if-else) ===

Numbers: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

Labels: ['odd', 'even', 'odd', 'even', 'odd', 'even', 'odd', 'even', 'odd', 'even']
Values: [-5, -2, 0, 3, 7]
Signs: ['negative', 'negative', 'zero', 'positive', 'positive']

Raw scores: [45, 92, 105, 78, 110, 88]
Capped at 100: [45, 92, 100, 78, 100, 88]

Numbers: [-5, 3, -2, 7, -8, 1]
Absolute: [5, 3, 2, 7, 8, 1]

=== Range-Based Comprehensions ===

First 10 squares: [0, 1, 4, 9, 16, 25, 36, 49, 64, 81]
Evens 0-20: [0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20]
Multiples of 5: [0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50]
Multiples of 5 (alt): [0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50]

=== Nested Lists (2D) ===

Matrix: [[1, 2, 3], [4, 5, 6], [7, 8, 9]]
Flattened: [1, 2, 3, 4, 5, 6, 7, 8, 9]

Multiplication table (3x3):
  [1, 2, 3]
  [2, 4, 6]
  [3, 6, 9]

=== Practical Example: Data Cleaning ===

Raw inputs: [' Alice ', 'BOB', '  charlie  ', 'DIANA', '  eve']
Cleaned: ['Alice', 'Bob', 'Charlie', 'Diana', 'Eve']

=== Practical Example: Temperature Conversion ===

Celsius: [0, 10, 20, 30, 40, 100]
Fahrenheit: [32.0, 50.0, 68.0, 86.0, 104.0, 212.0]

=== Practical Example: Extract Data ===

Student records:
  {'name': 'Alice', 'grade': 85}
  {'name': 'Bob', 'grade': 92}
  {'name': 'Charlie', 'grade': 78}
  {'name': 'Diana', 'grade': 95}

Names: ['Alice', 'Bob', 'Charlie', 'Diana']
Grades: [85, 92, 78, 95]
A students: ['Bob', 'Diana']

=== Practical Example: File Extensions ===

Files: ['photo.jpg', 'document.pdf', 'song.mp3', 'video.mp4', 'image.png']
Extensions: ['jpg', 'pdf', 'mp3', 'mp4', 'png']
Images: ['photo.jpg', 'image.png']

=== Practical Example: Generate Test Data ===

Test emails:
  user1@example.com
  user2@example.com
  user3@example.com
  user4@example.com
  user5@example.com
  user6@example.com
  user7@example.com
  user8@example.com
  user9@example.com
  user10@example.com

3x3 grid coordinates: [(0, 0), (0, 1), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2)]

=== Comparison: Comprehension vs Loop ===

Creating list of squares for 10,000 numbers...

List comprehension: 0.0008 seconds
Traditional loop:   0.0012 seconds

List comprehension is 1.5x faster!

✓ List comprehensions are more concise AND faster!
```

```python
# List Comprehensions: Pythonic List Creation

print("=" * 60)
print("LIST COMPREHENSIONS")
print("=" * 60)
print()

# ========================================
# BASIC TRANSFORMATIONS
# ========================================

print("=== Basic Transformations ===")
print()

# Traditional way
numbers = [1, 2, 3, 4, 5]
print(f"Original numbers: {numbers}")
print()

print("Traditional approach (loop + append):")
doubled_traditional = []
for num in numbers:
    doubled_traditional.append(num * 2)
print(f"  Code: for num in numbers: doubled.append(num * 2)")
print(f"  Result: {doubled_traditional}")

print()

print("List comprehension:")
doubled = [num * 2 for num in numbers]
print(f"  Code: [num * 2 for num in numbers]")
print(f"  Result: {doubled}")

print()

# More examples
squares = [x**2 for x in range(1, 11)]
print(f"Squares 1-10: {squares}")

cubes = [x**3 for x in range(1, 6)]
print(f"Cubes 1-5: {cubes}")

print()

# ========================================
# STRING OPERATIONS
# ========================================

print("=== String Operations ===")
print()

words = ["python", "java", "javascript", "ruby", "go"]
print(f"Original words: {words}")
print()

# Uppercase all
uppercase = [word.upper() for word in words]
print(f"Uppercase: {uppercase}")

# Capitalize first letter
capitalized = [word.capitalize() for word in words]
print(f"Capitalized: {capitalized}")

# Get first letter
first_letters = [word[0] for word in words]
print(f"First letters: {first_letters}")

# Get word lengths
lengths = [len(word) for word in words]
print(f"Lengths: {lengths}")

# Add exclamation mark
excited = [word + "!" for word in words]
print(f"Excited: {excited}")

print()

# ========================================
# FILTERING WITH IF
# ========================================

print("=== Filtering with if ===")
print()

numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
print(f"Numbers: {numbers}")
print()

# Even numbers only
evens = [num for num in numbers if num % 2 == 0]
print(f"Even numbers: {evens}")

# Odd numbers only
odds = [num for num in numbers if num % 2 != 0]
print(f"Odd numbers: {odds}")

# Numbers > 5
greater_than_5 = [num for num in numbers if num > 5]
print(f"Greater than 5: {greater_than_5}")

# Numbers divisible by 3
divisible_by_3 = [num for num in numbers if num % 3 == 0]
print(f"Divisible by 3: {divisible_by_3}")

print()

# Filter words
words = ["apple", "banana", "cat", "dog", "elephant", "fig"]
print(f"Words: {words}")

# Long words (>5 chars)
long_words = [word for word in words if len(word) > 5]
print(f"Long words (>5): {long_words}")

# Words starting with vowels
vowel_words = [word for word in words if word[0] in 'aeiou']
print(f"Start with vowel: {vowel_words}")

print()

# ========================================
# TRANSFORM + FILTER
# ========================================

print("=== Transform + Filter ===")
print()

numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
print(f"Numbers: {numbers}")
print()

# Square even numbers only
even_squares = [num**2 for num in numbers if num % 2 == 0]
print(f"Squares of evens: {even_squares}")

# Double odd numbers only
odd_doubled = [num * 2 for num in numbers if num % 2 != 0]
print(f"Doubled odds: {odd_doubled}")

print()

words = ["Python", "Java", "C", "JavaScript", "Go", "Ruby"]
print(f"Words: {words}")

# Uppercase long words
upper_long = [word.upper() for word in words if len(word) > 4]
print(f"Uppercase long: {upper_long}")

# Lowercase short words
lower_short = [word.lower() for word in words if len(word) <= 4]
print(f"Lowercase short: {lower_short}")

print()

# ========================================
# CONDITIONAL EXPRESSIONS (if-else)
# ========================================

print("=== Conditional Expressions (if-else) ===")
print()

numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
print(f"Numbers: {numbers}")
print()

# Label even/odd
labels = ["even" if num % 2 == 0 else "odd" for num in numbers]
print(f"Labels: {labels}")

# Positive/negative/zero
values = [-5, -2, 0, 3, 7]
signs = ["positive" if x > 0 else "negative" if x < 0 else "zero" for x in values]
print(f"Values: {values}")
print(f"Signs: {signs}")

print()

# Cap at maximum
scores = [45, 92, 105, 78, 110, 88]
print(f"Raw scores: {scores}")
capped = [score if score <= 100 else 100 for score in scores]
print(f"Capped at 100: {capped}")

print()

# Absolute values
numbers = [-5, 3, -2, 7, -8, 1]
print(f"Numbers: {numbers}")
absolute = [x if x >= 0 else -x for x in numbers]
print(f"Absolute: {absolute}")

print()

# ========================================
# RANGE-BASED COMPREHENSIONS
# ========================================

print("=== Range-Based Comprehensions ===")
print()

# First 10 squares
squares = [x**2 for x in range(10)]
print(f"First 10 squares: {squares}")

# Even numbers 0-20
evens = [x for x in range(21) if x % 2 == 0]
print(f"Evens 0-20: {evens}")

# Multiples of 5 up to 50
fives = [x for x in range(0, 51, 5)]
print(f"Multiples of 5: {fives}")

# Or using multiplication
fives_alt = [x * 5 for x in range(11)]
print(f"Multiples of 5 (alt): {fives_alt}")

print()

# ========================================
# NESTED LISTS (2D)
# ========================================

print("=== Nested Lists (2D) ===")
print()

# Flatten a 2D list
matrix = [[1, 2, 3], [4, 5, 6], [7, 8, 9]]
print(f"Matrix: {matrix}")

flattened = [num for row in matrix for num in row]
print(f"Flattened: {flattened}")

print()

# Create multiplication table
print("Multiplication table (3x3):")
table = [[row * col for col in range(1, 4)] for row in range(1, 4)]
for row in table:
    print(f"  {row}")

print()

# ========================================
# PRACTICAL EXAMPLES
# ========================================

print("=== Practical Example: Data Cleaning ===")
print()

# Clean user input
user_inputs = [" Alice ", "BOB", "  charlie  ", "DIANA", "  eve"]
print(f"Raw inputs: {user_inputs}")

# Clean: strip whitespace and capitalize
cleaned = [name.strip().capitalize() for name in user_inputs]
print(f"Cleaned: {cleaned}")

print()

print("=== Practical Example: Temperature Conversion ===")
print()

celsius = [0, 10, 20, 30, 40, 100]
print(f"Celsius: {celsius}")

fahrenheit = [(c * 9/5) + 32 for c in celsius]
print(f"Fahrenheit: {fahrenheit}")

print()

print("=== Practical Example: Extract Data ===")
print()

students = [
    {"name": "Alice", "grade": 85},
    {"name": "Bob", "grade": 92},
    {"name": "Charlie", "grade": 78},
    {"name": "Diana", "grade": 95}
]

print("Student records:")
for s in students:
    print(f"  {s}")

print()

# Extract all names
names = [student["name"] for student in students]
print(f"Names: {names}")

# Extract all grades
grades = [student["grade"] for student in students]
print(f"Grades: {grades}")

# Get names of students with A grades (>=90)
a_students = [student["name"] for student in students if student["grade"] >= 90]
print(f"A students: {a_students}")

print()

print("=== Practical Example: File Extensions ===")
print()

filenames = ["photo.jpg", "document.pdf", "song.mp3", "video.mp4", "image.png"]
print(f"Files: {filenames}")

# Extract extensions
extensions = [filename.split('.')[-1] for filename in filenames]
print(f"Extensions: {extensions}")

# Get image files only
images = [f for f in filenames if f.endswith(('.jpg', '.png'))]
print(f"Images: {images}")

print()

print("=== Practical Example: Generate Test Data ===")
print()

# Create 10 test user emails
emails = [f"user{i}@example.com" for i in range(1, 11)]
print("Test emails:")
for email in emails:
    print(f"  {email}")

print()

# Create coordinate pairs
coords = [(x, y) for x in range(3) for y in range(3)]
print(f"3x3 grid coordinates: {coords}")

print()

# ========================================
# COMPARISON: COMPREHENSION VS LOOP
# ========================================

print("=== Comparison: Comprehension vs Loop ===")
print()

import time

# Test data
test_range = range(10000)

print("Creating list of squares for 10,000 numbers...")
print()

# Method 1: List comprehension
start = time.time()
squares_comp = [x**2 for x in test_range]
comp_time = time.time() - start
print(f"List comprehension: {comp_time:.4f} seconds")

# Method 2: Traditional loop
start = time.time()
squares_loop = []
for x in test_range:
    squares_loop.append(x**2)
loop_time = time.time() - start
print(f"Traditional loop:   {loop_time:.4f} seconds")

print(f"\nList comprehension is {loop_time/comp_time:.1f}x faster!")

print()
print("✓ List comprehensions are more concise AND faster!")
```

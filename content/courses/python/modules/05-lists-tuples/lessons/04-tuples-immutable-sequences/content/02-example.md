---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
==================================================
TUPLES: IMMUTABLE SEQUENCES
==================================================

=== Creating Tuples ===

Point (with parens): (10, 20)
Type: <class 'tuple'>

Point (no parens): (10, 20)
Type: <class 'tuple'>

Student info: ('Alice', 20, 'Computer Science', 3.8)

Single-item tuple: (42,)
Type: <class 'tuple'>

Not a tuple: 42
Type: <class 'int'>

Empty tuple: ()
Length: 0

=== Accessing Tuple Elements ===

RGB color: (255, 128, 0)
Red component:   255
Green component: 128
Blue component:  0

Date: (2024, 1, 15)
Year (first):  2024
Day (last):    15
Month (middle): 1

=== Tuples are Immutable ===

Original coordinates: (40.7128, -74.006)

Trying to modify coords[0]...
ERROR: 'tuple' object does not support item assignment
✓ Confirmed: Tuples cannot be modified!

=== Tuple Unpacking ===

Point: (100, 200)
x = 100, y = 200

Person tuple: ('Bob', 30, 'Engineer')
Name: Bob
Age: 30
Job: Engineer

Before swap: a = 5, b = 10
After swap:  a = 10, b = 5

=== Tuple Methods ===

Numbers: (1, 2, 3, 2, 4, 2, 5)

Count of 2: 3
Count of 9: 0

First position of 3: 2
First position of 2: 1

=== Tuple Operations ===

tuple1: (1, 2, 3)
tuple2: (4, 5, 6)
Combined: (1, 2, 3, 4, 5, 6)

Pattern: ('X', 'O')
Repeated × 3: ('X', 'O', 'X', 'O', 'X', 'O')

Fruits: ('Apple', 'Banana', 'Cherry')
'Banana' in fruits: True
'Grape' in fruits: False

Data: (10, 20, 30, 40, 50)
Length: 5

=== Tuple Slicing ===

Original: ('A', 'B', 'C', 'D', 'E', 'F')
First 3: ('A', 'B', 'C')
Last 2: ('E', 'F')
Middle [2:5]: ('C', 'D', 'E')
Reversed: ('F', 'E', 'D', 'C', 'B', 'A')

=== Iterating Through Tuples ===

Days of work week:
  - Mon
  - Tue
  - Wed
  - Thu
  - Fri

With index:
  Day 1: Mon
  Day 2: Tue
  Day 3: Wed
  Day 4: Thu
  Day 5: Fri

=== Practical Example: Geographic Coordinates ===

New York City: (40.7128, -74.0060)
Los Angeles: (34.0522, -118.2437)
Chicago: (41.8781, -87.6298)
Houston: (29.7604, -95.3698)

Found: Los Angeles

=== Practical Example: RGB Colors ===

red     → RGB(255,   0,   0)
green   → RGB(  0, 255,   0)
blue    → RGB(  0,   0, 255)
yellow  → RGB(255, 255,   0)
purple  → RGB(128,   0, 128)
orange  → RGB(255, 165,   0)

=== Practical Example: Function Returns ===

Scores: [85, 92, 78, 95, 88, 90]

Stats tuple: (528, 6, 88.0, 78, 95)

Total: 528
Count: 6
Average: 88.0
Min: 78
Max: 95

=== Practical Example: Database Records ===

Student Records:
  ID 1: Alice    | CS      | GPA: 3.8
  ID 2: Bob      | Math    | GPA: 3.6
  ID 3: Charlie  | Physics | GPA: 3.9
  ID 4: Diana    | CS      | GPA: 3.7

CS Majors:
  Alice (GPA: 3.8)
  Diana (GPA: 3.7)

=== Converting Lists ↔ Tuples ===

List: [1, 2, 3, 4, 5]
Converted to tuple: (1, 2, 3, 4, 5)
Type: <class 'tuple'>

Tuple: ('A', 'B', 'C')
Converted to list: ['A', 'B', 'C']
Type: <class 'list'>

=== List vs Tuple Comparison ===

List:  [1, 2, 3, 4, 5]
Tuple: (1, 2, 3, 4, 5)

List size:  104 bytes
Tuple size: 80 bytes
Tuple uses 24 fewer bytes

List methods: ['append', 'clear', 'copy', 'count', 'extend', 'index', 'insert', 'pop']
Tuple methods: ['count', 'index']
```

```python
# Tuples: Immutable Sequences

print("=" * 50)
print("TUPLES: IMMUTABLE SEQUENCES")
print("=" * 50)
print()

# ========================================
# CREATING TUPLES
# ========================================

print("=== Creating Tuples ===")
print()

# With parentheses
point = (10, 20)
print(f"Point (with parens): {point}")
print(f"Type: {type(point)}")

print()

# Without parentheses (tuple packing)
point2 = 10, 20
print(f"Point (no parens): {point2}")
print(f"Type: {type(point2)}")

print()

# Multiple types
student = ("Alice", 20, "Computer Science", 3.8)
print(f"Student info: {student}")

print()

# Single-item tuple (comma required!)
single = (42,)
print(f"Single-item tuple: {single}")
print(f"Type: {type(single)}")

print()

# NOT a tuple (just parentheses for grouping)
not_tuple = (42)
print(f"Not a tuple: {not_tuple}")
print(f"Type: {type(not_tuple)}")

print()

# Empty tuple
empty = ()
print(f"Empty tuple: {empty}")
print(f"Length: {len(empty)}")

print()

# ========================================
# ACCESSING TUPLE ELEMENTS
# ========================================

print("=== Accessing Tuple Elements ===")
print()

rgb = (255, 128, 0)  # Orange color
print(f"RGB color: {rgb}")
print(f"Red component:   {rgb[0]}")
print(f"Green component: {rgb[1]}")
print(f"Blue component:  {rgb[2]}")

print()

# Negative indexing
date = (2024, 1, 15)  # January 15, 2024
print(f"Date: {date}")
print(f"Year (first):  {date[0]}")
print(f"Day (last):    {date[-1]}")
print(f"Month (middle): {date[1]}")

print()

# ========================================
# TUPLES ARE IMMUTABLE
# ========================================

print("=== Tuples are Immutable ===")
print()

coords = (40.7128, -74.0060)
print(f"Original coordinates: {coords}")

print("\nTrying to modify coords[0]...")
try:
    coords[0] = 50.0
except TypeError as e:
    print(f"ERROR: {e}")
    print("✓ Confirmed: Tuples cannot be modified!")

print()

# ========================================
# TUPLE UNPACKING
# ========================================

print("=== Tuple Unpacking ===")
print()

# Basic unpacking
point = (100, 200)
x, y = point
print(f"Point: {point}")
print(f"x = {x}, y = {y}")

print()

# Multiple values
person = ("Bob", 30, "Engineer")
name, age, job = person
print(f"Person tuple: {person}")
print(f"Name: {name}")
print(f"Age: {age}")
print(f"Job: {job}")

print()

# Swapping variables (Pythonic way!)
a = 5
b = 10
print(f"Before swap: a = {a}, b = {b}")
a, b = b, a  # Swap using tuple packing/unpacking
print(f"After swap:  a = {a}, b = {b}")

print()

# ========================================
# TUPLE METHODS
# ========================================

print("=== Tuple Methods ===")
print()

numbers = (1, 2, 3, 2, 4, 2, 5)
print(f"Numbers: {numbers}")
print()

# count() - count occurrences
count_2 = numbers.count(2)
print(f"Count of 2: {count_2}")

count_9 = numbers.count(9)
print(f"Count of 9: {count_9}")

print()

# index() - find first occurrence
position = numbers.index(3)
print(f"First position of 3: {position}")

position_2 = numbers.index(2)
print(f"First position of 2: {position_2}")

print()

# ========================================
# TUPLE OPERATIONS
# ========================================

print("=== Tuple Operations ===")
print()

# Concatenation
tuple1 = (1, 2, 3)
tuple2 = (4, 5, 6)
combined = tuple1 + tuple2
print(f"tuple1: {tuple1}")
print(f"tuple2: {tuple2}")
print(f"Combined: {combined}")

print()

# Repetition
pattern = ("X", "O")
repeated = pattern * 3
print(f"Pattern: {pattern}")
print(f"Repeated × 3: {repeated}")

print()

# Membership
fruits = ("Apple", "Banana", "Cherry")
print(f"Fruits: {fruits}")
print(f"'Banana' in fruits: {'Banana' in fruits}")
print(f"'Grape' in fruits: {'Grape' in fruits}")

print()

# Length
data = (10, 20, 30, 40, 50)
print(f"Data: {data}")
print(f"Length: {len(data)}")

print()

# ========================================
# TUPLE SLICING
# ========================================

print("=== Tuple Slicing ===")
print()

letters = ('A', 'B', 'C', 'D', 'E', 'F')
print(f"Original: {letters}")

first_three = letters[:3]
print(f"First 3: {first_three}")

last_two = letters[-2:]
print(f"Last 2: {last_two}")

middle = letters[2:5]
print(f"Middle [2:5]: {middle}")

reversed_tuple = letters[::-1]
print(f"Reversed: {reversed_tuple}")

print()

# ========================================
# ITERATING THROUGH TUPLES
# ========================================

print("=== Iterating Through Tuples ===")
print()

days = ("Mon", "Tue", "Wed", "Thu", "Fri")

print("Days of work week:")
for day in days:
    print(f"  - {day}")

print()

print("With index:")
for i, day in enumerate(days, start=1):
    print(f"  Day {i}: {day}")

print()

# ========================================
# PRACTICAL EXAMPLES
# ========================================

print("=== Practical Example: Geographic Coordinates ===")
print()

cities = {
    (40.7128, -74.0060): "New York City",
    (34.0522, -118.2437): "Los Angeles",
    (41.8781, -87.6298): "Chicago",
    (29.7604, -95.3698): "Houston"
}

for coords, city in cities.items():
    lat, lon = coords  # Unpack tuple
    print(f"{city}: ({lat:.4f}, {lon:.4f})")

print()

# Search for coordinates
search_coords = (34.0522, -118.2437)
if search_coords in cities:
    print(f"Found: {cities[search_coords]}")

print()

print("=== Practical Example: RGB Colors ===")
print()

colors = {
    "red":    (255, 0, 0),
    "green":  (0, 255, 0),
    "blue":   (0, 0, 255),
    "yellow": (255, 255, 0),
    "purple": (128, 0, 128),
    "orange": (255, 165, 0)
}

for color_name, rgb in colors.items():
    r, g, b = rgb  # Unpack tuple
    print(f"{color_name:7} → RGB({r:3}, {g:3}, {b:3})")

print()

print("=== Practical Example: Function Returns ===")
print()

def get_stats(numbers):
    """Return multiple statistics as a tuple"""
    total = sum(numbers)
    count = len(numbers)
    average = total / count
    minimum = min(numbers)
    maximum = max(numbers)
    
    return (total, count, average, minimum, maximum)

scores = [85, 92, 78, 95, 88, 90]
print(f"Scores: {scores}")
print()

# Get all stats at once
stats = get_stats(scores)
print(f"Stats tuple: {stats}")
print()

# Unpack into variables
total, count, avg, min_score, max_score = get_stats(scores)
print(f"Total: {total}")
print(f"Count: {count}")
print(f"Average: {avg:.1f}")
print(f"Min: {min_score}")
print(f"Max: {max_score}")

print()

print("=== Practical Example: Database Records ===")
print()

# Each record is a tuple (immutable)
students = [
    (1, "Alice", "CS", 3.8),
    (2, "Bob", "Math", 3.6),
    (3, "Charlie", "Physics", 3.9),
    (4, "Diana", "CS", 3.7)
]

print("Student Records:")
for student in students:
    id, name, major, gpa = student  # Unpack
    print(f"  ID {id}: {name:8} | {major:7} | GPA: {gpa}")

print()

# Find CS majors
print("CS Majors:")
for id, name, major, gpa in students:
    if major == "CS":
        print(f"  {name} (GPA: {gpa})")

print()

# ========================================
# CONVERTING BETWEEN LISTS AND TUPLES
# ========================================

print("=== Converting Lists ↔ Tuples ===")
print()

my_list = [1, 2, 3, 4, 5]
print(f"List: {my_list}")

# List → Tuple
my_tuple = tuple(my_list)
print(f"Converted to tuple: {my_tuple}")
print(f"Type: {type(my_tuple)}")

print()

my_tuple2 = ('A', 'B', 'C')
print(f"Tuple: {my_tuple2}")

# Tuple → List
my_list2 = list(my_tuple2)
print(f"Converted to list: {my_list2}")
print(f"Type: {type(my_list2)}")

print()

# ========================================
# COMPARISON
# ========================================

print("=== List vs Tuple Comparison ===")
print()

import sys

my_list = [1, 2, 3, 4, 5]
my_tuple = (1, 2, 3, 4, 5)

print(f"List:  {my_list}")
print(f"Tuple: {my_tuple}")
print()

print(f"List size:  {sys.getsizeof(my_list)} bytes")
print(f"Tuple size: {sys.getsizeof(my_tuple)} bytes")
print(f"Tuple uses {sys.getsizeof(my_list) - sys.getsizeof(my_tuple)} fewer bytes")

print()

print("List methods:", [m for m in dir(my_list) if not m.startswith('_')][:8])
print("Tuple methods:", [m for m in dir(my_tuple) if not m.startswith('_')])
```

---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Creating Lists ===

Fruits: ['Apple', 'Banana', 'Cherry', 'Date', 'Elderberry']
Scores: [85, 92, 78, 95, 88]
Student info: ['Alice', 20, 'Computer Science', 3.8, True]
Empty list: []

=== List Length ===

Number of fruits: 5
Number of scores: 5
Number of items in empty list: 0

=== Accessing Items (Positive Indexing) ===

Fruits list: ['Apple', 'Banana', 'Cherry', 'Date', 'Elderberry']
  Index 0 (first):  Apple
  Index 1 (second): Banana
  Index 2 (third):  Cherry
  Index 3 (fourth): Date
  Index 4 (fifth):  Elderberry

=== Accessing Items (Negative Indexing) ===

Fruits list: ['Apple', 'Banana', 'Cherry', 'Date', 'Elderberry']
  Index -1 (last):        Elderberry
  Index -2 (2nd to last): Date
  Index -3 (3rd to last): Cherry
  Index -4 (4th to last): Banana
  Index -5 (5th to last): Apple

=== Index Map ===

Positive indexing:
  fruits[0] = Apple
  fruits[1] = Banana
  fruits[2] = Cherry
  fruits[3] = Date
  fruits[4] = Elderberry

Negative indexing:
  fruits[-1] = Elderberry
  fruits[-2] = Date
  fruits[-3] = Cherry
  fruits[-4] = Banana
  fruits[-5] = Apple

=== Using Variables as Indices ===

Item at position 2: Cherry
Last index is 4
Last item: Elderberry

=== Index Validation ===

List has 5 items (indices 0-4)
Is index 10 valid? False
  ❌ Index 10 is out of range!

=== Common Access Patterns ===

Playlist: ['Song A', 'Song B', 'Song C', 'Song D', 'Song E']
  First song: Song A
  Last song:  Song E
  Middle song: Song C

=== Iterating Through a List ===

Method 1: Direct iteration
  - Apple
  - Banana
  - Cherry
  - Date
  - Elderberry

Method 2: Using indices
  1. Apple
  2. Banana
  3. Cherry
  4. Date
  5. Elderberry

Method 3: Using enumerate (index + value)
  Index 0: Apple
  Index 1: Banana
  Index 2: Cherry
  Index 3: Date
  Index 4: Elderberry

=== Practical Example: Test Scores ===

Test scores: [85, 92, 78, 95, 88, 90, 76, 94]
Total tests: 8
First test: 85
Most recent test: 94
Average score: 87.2
Highest score: 95
Passing scores (>=80): 6/8
```

```python
# List Basics: Ordered Collections

# Creating Lists
print("=== Creating Lists ===")
print()

# List of strings
fruits = ["Apple", "Banana", "Cherry", "Date", "Elderberry"]
print(f"Fruits: {fruits}")

# List of numbers
scores = [85, 92, 78, 95, 88]
print(f"Scores: {scores}")

# List of mixed types
student = ["Alice", 20, "Computer Science", 3.8, True]
print(f"Student info: {student}")

# Empty list
empty_list = []
print(f"Empty list: {empty_list}")

print()

# List Length
print("=== List Length ===")
print()

print(f"Number of fruits: {len(fruits)}")
print(f"Number of scores: {len(scores)}")
print(f"Number of items in empty list: {len(empty_list)}")

print()

# Accessing Items (Positive Indexing)
print("=== Accessing Items (Positive Indexing) ===")
print()

print("Fruits list:", fruits)
print(f"  Index 0 (first):  {fruits[0]}")
print(f"  Index 1 (second): {fruits[1]}")
print(f"  Index 2 (third):  {fruits[2]}")
print(f"  Index 3 (fourth): {fruits[3]}")
print(f"  Index 4 (fifth):  {fruits[4]}")

print()

# Accessing Items (Negative Indexing)
print("=== Accessing Items (Negative Indexing) ===")
print()

print("Fruits list:", fruits)
print(f"  Index -1 (last):        {fruits[-1]}")
print(f"  Index -2 (2nd to last): {fruits[-2]}")
print(f"  Index -3 (3rd to last): {fruits[-3]}")
print(f"  Index -4 (4th to last): {fruits[-4]}")
print(f"  Index -5 (5th to last): {fruits[-5]}")

print()

# Visual Index Map
print("=== Index Map ===")
print()

print("Positive indexing:")
for i in range(len(fruits)):
    print(f"  fruits[{i}] = {fruits[i]}")

print()
print("Negative indexing:")
for i in range(-1, -len(fruits)-1, -1):
    print(f"  fruits[{i}] = {fruits[i]}")

print()

# Using Variables as Indices
print("=== Using Variables as Indices ===")
print()

position = 2
print(f"Item at position {position}: {fruits[position]}")

last_index = len(fruits) - 1  # Last valid index
print(f"Last index is {last_index}")
print(f"Last item: {fruits[last_index]}")

print()

# Checking if Index is Valid
print("=== Index Validation ===")
print()

test_index = 10
print(f"List has {len(fruits)} items (indices 0-{len(fruits)-1})")
print(f"Is index {test_index} valid? {test_index < len(fruits)}")

if test_index < len(fruits):
    print(f"  Item at index {test_index}: {fruits[test_index]}")
else:
    print(f"  ❌ Index {test_index} is out of range!")

print()

# Common Patterns
print("=== Common Access Patterns ===")
print()

playlist = ["Song A", "Song B", "Song C", "Song D", "Song E"]

print("Playlist:", playlist)
print(f"  First song: {playlist[0]}")
print(f"  Last song:  {playlist[-1]}")
print(f"  Middle song: {playlist[len(playlist)//2]}")

print()

# Iterating Through a List
print("=== Iterating Through a List ===")
print()

print("Method 1: Direct iteration")
for fruit in fruits:
    print(f"  - {fruit}")

print()
print("Method 2: Using indices")
for i in range(len(fruits)):
    print(f"  {i+1}. {fruits[i]}")

print()
print("Method 3: Using enumerate (index + value)")
for index, fruit in enumerate(fruits):
    print(f"  Index {index}: {fruit}")

print()

# Practical Example: Test Scores
print("=== Practical Example: Test Scores ===")
print()

test_scores = [85, 92, 78, 95, 88, 90, 76, 94]

print(f"Test scores: {test_scores}")
print(f"Total tests: {len(test_scores)}")
print(f"First test: {test_scores[0]}")
print(f"Most recent test: {test_scores[-1]}")

# Calculate average
total = 0
for score in test_scores:
    total = total + score
average = total / len(test_scores)

print(f"Average score: {average:.1f}")

# Find highest score
highest = test_scores[0]
for score in test_scores:
    if score > highest:
        highest = score

print(f"Highest score: {highest}")

# Count passing scores (>=80)
passing = 0
for score in test_scores:
    if score >= 80:
        passing = passing + 1

print(f"Passing scores (>=80): {passing}/{len(test_scores)}")
```

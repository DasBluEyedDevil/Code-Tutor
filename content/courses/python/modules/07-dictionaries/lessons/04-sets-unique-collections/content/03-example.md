---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Creating Sets ===
Fruits: {'cherry', 'banana', 'apple'}
From list with duplicates: {1, 2, 3}
Unique letters in 'mississippi': {'m', 'i', 's', 'p'}

=== Adding and Removing ===
After add: {'cherry', 'banana', 'apple', 'date'}
After remove: {'cherry', 'apple', 'date'}

=== Set Operations ===
Set A: {1, 2, 3, 4, 5}
Set B: {4, 5, 6, 7, 8}

Union (A | B): {1, 2, 3, 4, 5, 6, 7, 8}
Intersection (A & B): {4, 5}
Difference (A - B): {1, 2, 3}
Symmetric Difference (A ^ B): {1, 2, 3, 6, 7, 8}

=== Practical: Remove Duplicates ===
With duplicates: ['apple', 'banana', 'apple', 'cherry', 'banana', 'date']
Unique items: ['apple', 'banana', 'cherry', 'date']

=== Practical: Find Common Items ===
Alice's skills: {'Python', 'SQL', 'JavaScript'}
Bob's skills: {'Python', 'Java', 'SQL'}
Common skills: {'Python', 'SQL'}
All skills: {'Python', 'SQL', 'JavaScript', 'Java'}
```

```python
# Sets - Unique Collections

print("=== Creating Sets ===")

# Using curly braces
fruits = {"apple", "banana", "cherry"}
print(f"Fruits: {fruits}")

# From a list (removes duplicates)
numbers = set([1, 2, 2, 3, 3, 3])
print(f"From list with duplicates: {numbers}")

# From a string
letters = set("mississippi")
print(f"Unique letters in 'mississippi': {letters}")

print("\n=== Adding and Removing ===")

fruits = {"apple", "banana", "cherry"}

# Add one item
fruits.add("date")
print(f"After add: {fruits}")

# Remove an item
fruits.remove("banana")
print(f"After remove: {fruits}")

print("\n=== Set Operations ===")

a = {1, 2, 3, 4, 5}
b = {4, 5, 6, 7, 8}

print(f"Set A: {a}")
print(f"Set B: {b}")

# Union - all items from both sets
print(f"\nUnion (A | B): {a | b}")

# Intersection - items in BOTH sets
print(f"Intersection (A & B): {a & b}")

# Difference - items in A but not in B
print(f"Difference (A - B): {a - b}")

# Symmetric difference - items in either, but not both
print(f"Symmetric Difference (A ^ B): {a ^ b}")

print("\n=== Practical: Remove Duplicates ===")

with_duplicates = ["apple", "banana", "apple", "cherry", "banana", "date"]
unique_items = list(set(with_duplicates))

print(f"With duplicates: {with_duplicates}")
print(f"Unique items: {sorted(unique_items)}")

print("\n=== Practical: Find Common Items ===")

alice_skills = {"Python", "JavaScript", "SQL"}
bob_skills = {"Python", "Java", "SQL"}

print(f"Alice's skills: {alice_skills}")
print(f"Bob's skills: {bob_skills}")
print(f"Common skills: {alice_skills & bob_skills}")
print(f"All skills: {alice_skills | bob_skills}")
```

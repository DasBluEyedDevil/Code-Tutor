---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Starting List ===

Original list: ['Apple', 'Banana', 'Cherry']
Length: 3

=== Adding Items ===

Using append('Date'):
  After append: ['Apple', 'Banana', 'Cherry', 'Date']

Using insert(1, 'Avocado'):
  After insert: ['Apple', 'Avocado', 'Banana', 'Cherry', 'Date']

Using extend(['Fig', 'Grape']):
  After extend: ['Apple', 'Avocado', 'Banana', 'Cherry', 'Date', 'Fig', 'Grape']

=== Removing Items ===

Current list: ['Apple', 'Avocado', 'Banana', 'Cherry', 'Date', 'Fig', 'Grape']

Using remove('Avocado'):
  After remove: ['Apple', 'Banana', 'Cherry', 'Date', 'Fig', 'Grape']

Using pop():
  Popped item: Grape
  After pop: ['Apple', 'Banana', 'Cherry', 'Date', 'Fig']

Using pop(0):
  Popped item: Apple
  After pop(0): ['Banana', 'Cherry', 'Date', 'Fig']

=== Organizing Lists ===

Original numbers: [5, 2, 8, 1, 9, 3, 7]

Using sort():
  After sort: [1, 2, 3, 5, 7, 8, 9]

Using sort(reverse=True):
  After reverse sort: [9, 8, 7, 5, 3, 2, 1]

Using reverse():
  After reverse: [1, 2, 3, 5, 7, 8, 9]

Original words: ['Zebra', 'Apple', 'Mango', 'Banana']
Sorted words: ['Apple', 'Banana', 'Mango', 'Zebra']

=== Finding & Counting ===

Scores: [85, 92, 78, 92, 88, 92, 95]

Using index(92):
  First 92 is at index: 1

Using count(92):
  Number of 92s: 3

Using count(100):
  Number of 100s: 0

=== List Operators ===

list1: [1, 2, 3]
list2: [4, 5, 6]
list1 + list2: [1, 2, 3, 4, 5, 6]

pattern: ['X', 'O']
pattern * 3: ['X', 'O', 'X', 'O', 'X', 'O']

Groceries: ['Milk', 'Eggs', 'Bread', 'Butter']
'Milk' in groceries: True
'Cheese' in groceries: False
'Cheese' not in groceries: True

=== Practical Example: Task Manager ===

Adding tasks...
Tasks: ['Study Python', 'Exercise', 'Call mom', 'Buy groceries']

Completed: Study Python
Remaining: ['Exercise', 'Call mom', 'Buy groceries']

After adding urgent task: ['Fix bug (URGENT!)', 'Exercise', 'Call mom', 'Buy groceries']

'Exercise' found at position 1

=== Practical Example: Shopping Cart ===

Cart: ['Laptop', 'Mouse', 'Keyboard', 'Mouse', 'Monitor', 'Mouse']
Number of mice in cart: 3
After removing one mouse: ['Laptop', 'Keyboard', 'Mouse', 'Monitor', 'Mouse']

Cart sorted: ['Keyboard', 'Laptop', 'Monitor', 'Mouse', 'Mouse']

=== Common Pitfalls ===

WRONG: result = nums.append(4)
  result is: None
  nums is: [1, 2, 3, 4]

List: [1, 2, 3]
Trying to remove(5)...
  ERROR: list.remove(x): x not in list
  Safe: 5 not in list, skipping remove

Original: [3, 1, 2]
After original.sort(): [1, 2, 3]

Original2: [3, 1, 2]
sorted(original2): [1, 2, 3]
Original2 unchanged: [3, 1, 2]
```

```python
# List Methods & Operations: Modifying Lists

# Starting List
print("=== Starting List ===")
print()

fruits = ["Apple", "Banana", "Cherry"]
print(f"Original list: {fruits}")
print(f"Length: {len(fruits)}")

print()

# ========================================
# ADDING ITEMS
# ========================================

print("=== Adding Items ===")
print()

# append() - add to end
print("Using append('Date'):")
fruits.append("Date")
print(f"  After append: {fruits}")

# insert() - add at specific position
print("\nUsing insert(1, 'Avocado'):")
fruits.insert(1, "Avocado")  # Insert at index 1
print(f"  After insert: {fruits}")

# extend() - add multiple items
print("\nUsing extend(['Fig', 'Grape']):")
fruits.extend(["Fig", "Grape"])
print(f"  After extend: {fruits}")

print()

# ========================================
# REMOVING ITEMS
# ========================================

print("=== Removing Items ===")
print()

print(f"Current list: {fruits}")
print()

# remove() - remove first occurrence
print("Using remove('Avocado'):")
fruits.remove("Avocado")
print(f"  After remove: {fruits}")

# pop() - remove and return last item
print("\nUsing pop():")
last_item = fruits.pop()
print(f"  Popped item: {last_item}")
print(f"  After pop: {fruits}")

# pop(index) - remove and return item at index
print("\nUsing pop(0):")
first_item = fruits.pop(0)
print(f"  Popped item: {first_item}")
print(f"  After pop(0): {fruits}")

print()

# ========================================
# ORGANIZING
# ========================================

print("=== Organizing Lists ===")
print()

# Create new list for sorting examples
numbers = [5, 2, 8, 1, 9, 3, 7]
print(f"Original numbers: {numbers}")

# sort() - sort in place (ascending)
print("\nUsing sort():")
numbers.sort()
print(f"  After sort: {numbers}")

# sort(reverse=True) - descending order
print("\nUsing sort(reverse=True):")
numbers.sort(reverse=True)
print(f"  After reverse sort: {numbers}")

# reverse() - reverse current order
print("\nUsing reverse():")
numbers.reverse()
print(f"  After reverse: {numbers}")

print()

# Sorting strings
words = ["Zebra", "Apple", "Mango", "Banana"]
print(f"Original words: {words}")
words.sort()
print(f"Sorted words: {words}")

print()

# ========================================
# FINDING & COUNTING
# ========================================

print("=== Finding & Counting ===")
print()

scores = [85, 92, 78, 92, 88, 92, 95]
print(f"Scores: {scores}")
print()

# index() - find position of item
print("Using index(92):")
position = scores.index(92)
print(f"  First 92 is at index: {position}")

# count() - count occurrences
print("\nUsing count(92):")
count_92 = scores.count(92)
print(f"  Number of 92s: {count_92}")

print("\nUsing count(100):")
count_100 = scores.count(100)
print(f"  Number of 100s: {count_100}")

print()

# ========================================
# LIST OPERATORS
# ========================================

print("=== List Operators ===")
print()

# Concatenation (+)
list1 = [1, 2, 3]
list2 = [4, 5, 6]
print(f"list1: {list1}")
print(f"list2: {list2}")
print(f"list1 + list2: {list1 + list2}")

print()

# Repetition (*)
pattern = ["X", "O"]
print(f"pattern: {pattern}")
print(f"pattern * 3: {pattern * 3}")

print()

# Membership (in)
groceries = ["Milk", "Eggs", "Bread", "Butter"]
print(f"Groceries: {groceries}")
print(f"'Milk' in groceries: {'Milk' in groceries}")
print(f"'Cheese' in groceries: {'Cheese' in groceries}")
print(f"'Cheese' not in groceries: {'Cheese' not in groceries}")

print()

# ========================================
# PRACTICAL EXAMPLES
# ========================================

print("=== Practical Example: Task Manager ===")
print()

tasks = []

# Add tasks
print("Adding tasks...")
tasks.append("Study Python")
tasks.append("Exercise")
tasks.append("Call mom")
tasks.append("Buy groceries")
print(f"Tasks: {tasks}")

print()

# Complete a task
completed = tasks.pop(0)  # Remove first task
print(f"Completed: {completed}")
print(f"Remaining: {tasks}")

print()

# Add urgent task at beginning
tasks.insert(0, "Fix bug (URGENT!)")
print(f"After adding urgent task: {tasks}")

print()

# Check if task exists
task_to_find = "Exercise"
if task_to_find in tasks:
    position = tasks.index(task_to_find)
    print(f"'{task_to_find}' found at position {position}")
else:
    print(f"'{task_to_find}' not in list")

print()

print("=== Practical Example: Shopping Cart ===")
print()

cart = ["Laptop", "Mouse", "Keyboard", "Mouse", "Monitor", "Mouse"]
print(f"Cart: {cart}")

# Count how many mice
mouse_count = cart.count("Mouse")
print(f"Number of mice in cart: {mouse_count}")

# Remove one mouse
if "Mouse" in cart:
    cart.remove("Mouse")  # Removes first occurrence
    print(f"After removing one mouse: {cart}")

print()

# Sort items alphabetically
cart.sort()
print(f"Cart sorted: {cart}")

print()

# ========================================
# COMMON PITFALLS
# ========================================

print("=== Common Pitfalls ===")
print()

# Pitfall 1: Using append() return value
nums = [1, 2, 3]
print("WRONG: result = nums.append(4)")
result = nums.append(4)
print(f"  result is: {result}")  # None!
print(f"  nums is: {nums}")      # [1, 2, 3, 4]

print()

# Pitfall 2: remove() on non-existent item
test_list = [1, 2, 3]
print(f"List: {test_list}")
print("Trying to remove(5)...")
try:
    test_list.remove(5)
except ValueError as e:
    print(f"  ERROR: {e}")

# Safe removal
if 5 in test_list:
    test_list.remove(5)
else:
    print("  Safe: 5 not in list, skipping remove")

print()

# Pitfall 3: Modifying vs Creating New
original = [3, 1, 2]
print(f"Original: {original}")

# sort() modifies in place
original.sort()
print(f"After original.sort(): {original}")  # Changed!

print()

# To keep original, use sorted()
original2 = [3, 1, 2]
print(f"Original2: {original2}")
sorted_copy = sorted(original2)  # Returns new list
print(f"sorted(original2): {sorted_copy}")
print(f"Original2 unchanged: {original2}")
```

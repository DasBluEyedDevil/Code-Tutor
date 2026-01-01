---
type: "THEORY"
title: "Syntax Breakdown"
---

### List Creation Syntax:
```
# Empty list
my_list = []

# List with values
my_list = [value1, value2, value3]

# Examples:
numbers = [1, 2, 3, 4, 5]
words = ["hello", "world"]
mixed = [1, "two", 3.0, True]

```
### Accessing Elements:
```
my_list[index]  # Get item at index

# Positive indices (0-based):
my_list[0]   # First item
my_list[1]   # Second item
my_list[2]   # Third item

# Negative indices (from end):
my_list[-1]  # Last item
my_list[-2]  # Second to last
my_list[-3]  # Third to last

```
### Index Diagram:
<pre>List: ["A", "B", "C", "D", "E"]

Positive:  0    1    2    3    4
          ["A", "B", "C", "D", "E"]
Negative: -5   -4   -3   -2   -1

Rules:
  - First index: 0 (or -len)
  - Last index:  len-1 (or -1)
  - Valid range: 0 to len(list)-1
</pre>### Common Operations:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Operation</th><th>Syntax</th><th>Example</th><th>Result</th></tr><tr><td>Create list</td><td>[item1, item2, ...]</td><td>[1, 2, 3]</td><td>[1, 2, 3]</td></tr><tr><td>Get length</td><td>len(list)</td><td>len([1,2,3])</td><td>3</td></tr><tr><td>Access item</td><td>list[index]</td><td>nums[0]</td><td>First item</td></tr><tr><td>Last item</td><td>list[-1]</td><td>nums[-1]</td><td>Last item</td></tr><tr><td>First item</td><td>list[0]</td><td>nums[0]</td><td>First item</td></tr></table>### Index Calculation:
```
# For a list with 5 items:
length = 5

# Valid positive indices: 0, 1, 2, 3, 4
first_index = 0
last_index = length - 1  # 4

# Valid negative indices: -5, -4, -3, -2, -1
first_negative = -length  # -5
last_negative = -1

# Middle index (for odd-length lists)
middle = length // 2  # 2 (third item)

```
### Iterating Patterns:
#### Pattern 1: Direct Iteration (Most Common)
```
for item in my_list:
    print(item)

# Use when: You just need each value
# Example: Print each name

```
#### Pattern 2: Index-Based Iteration
```
for i in range(len(my_list)):
    print(my_list[i])

# Use when: You need the index number
# Example: "Item 1: Apple, Item 2: Banana"

```
#### Pattern 3: enumerate() - Best of Both
```
for index, value in enumerate(my_list):
    print(f"{index}: {value}")

# Use when: You need both index AND value
# Example: Show position and item

```
#### Pattern 4: enumerate with Custom Start
```
for position, item in enumerate(my_list, start=1):
    print(f"#{position}: {item}")

# Use when: You want 1-based numbering
# Example: "#1: First, #2: Second"

```
### Common Mistakes:
#### 1. IndexError (Out of Range)
```
# WRONG:
fruits = ["Apple", "Banana", "Cherry"]
print(fruits[3])  # ERROR! Only indices 0, 1, 2 exist

# CORRECT:
if 3 < len(fruits):
    print(fruits[3])
else:
    print("Index out of range!")

# OR use try/except:
try:
    print(fruits[3])
except IndexError:
    print("No item at that index!")

```
#### 2. Off-by-One Error (len vs len-1)
```
# WRONG:
for i in range(len(fruits)):
    print(fruits[i+1])  # Crashes on last iteration!

# CORRECT:
for i in range(len(fruits)):
    print(fruits[i])  # i goes from 0 to len-1

```
#### 3. Confusing Index 1 with First Item
```
# WRONG (thinking 1-based):
first = fruits[1]  # Actually gets SECOND item!

# CORRECT (zero-based):
first = fruits[0]  # First item

```
#### 4. Forgetting Negative Index Behavior
```
fruits = ["A", "B", "C"]

print(fruits[-0])  # Same as fruits[0]! (A)
print(fruits[-1])  # Last item (C)
print(fruits[-4])  # ERROR! Only -3, -2, -1 valid

```
### Valid Index Ranges:
```
# For a list of length n:

# Positive indices: 0 to n-1
fruits = ["A", "B", "C"]  # length = 3
# Valid: 0, 1, 2
# Invalid: 3, 4, 5, ...

# Negative indices: -n to -1
# Valid: -3, -2, -1
# Invalid: -4, -5, -6, ...

# Check validity:
index = 5
if -len(fruits) <= index < len(fruits):
    print(fruits[index])
else:
    print("Invalid index!")

```
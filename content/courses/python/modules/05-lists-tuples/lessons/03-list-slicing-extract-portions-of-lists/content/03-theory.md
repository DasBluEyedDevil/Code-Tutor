---
type: "THEORY"
title: "Syntax Breakdown"
---

### Slice Notation: [start:stop:step]
```
my_list[start:stop:step]

Parameters:
  - start: Index to begin (INCLUSIVE) - defaults to 0
  - stop:  Index to end (EXCLUSIVE!) - defaults to len(list)
  - step:  Increment between indices - defaults to 1

Note: All three parameters are OPTIONAL!

```
### Common Slice Patterns:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Pattern</th><th>Syntax</th><th>Meaning</th><th>Example</th></tr><tr><td>First n</td><td>[:n]</td><td>From start to index n-1</td><td>nums[:3] → first 3</td></tr><tr><td>Last n</td><td>[-n:]</td><td>Last n elements</td><td>nums[-3:] → last 3</td></tr><tr><td>From index i</td><td>[i:]</td><td>From index i to end</td><td>nums[5:] → from 5 onward</td></tr><tr><td>Until index i</td><td>[:i]</td><td>From start to i-1</td><td>nums[:5] → up to index 4</td></tr><tr><td>Range i to j</td><td>[i:j]</td><td>From i to j-1</td><td>nums[2:7] → indices 2-6</td></tr><tr><td>All except last</td><td>[:-1]</td><td>Everything but last</td><td>nums[:-1]</td></tr><tr><td>All except first</td><td>[1:]</td><td>Everything but first</td><td>nums[1:]</td></tr><tr><td>Copy list</td><td>[:]</td><td>Shallow copy</td><td>copy = nums[:]</td></tr><tr><td>Every nth</td><td>[::n]</td><td>Every nth element</td><td>nums[::2] → every 2nd</td></tr><tr><td>Reverse</td><td>[::-1]</td><td>All elements reversed</td><td>nums[::-1]</td></tr></table>### Detailed Examples:
#### Basic Slicing [start:stop]
```
nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

# Get items from index 2 to 5 (NOT 6!)
slice1 = nums[2:6]  # [2, 3, 4, 5]

# Visualization:
#  Index:  0  1  2  3  4  5  6  7  8  9
#  Value:  0  1  2  3  4  5  6  7  8  9
#              ^           ^
#           start=2     stop=6 (NOT included!)
#  Result: [2, 3, 4, 5]

```
#### Omitting Parameters
```
nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

# Omit start (defaults to 0)
nums[:5]   # [0, 1, 2, 3, 4]
# Same as: nums[0:5]

# Omit stop (defaults to end)
nums[5:]   # [5, 6, 7, 8, 9]
# Same as: nums[5:10] or nums[5:len(nums)]

# Omit both (copy entire list)
nums[:]    # [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
# Same as: nums[0:10] or nums[0:len(nums)]

```
#### Negative Indices
```
nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

# Last 3 items
nums[-3:]  # [7, 8, 9]
# Starts at -3 (third from end), goes to end

# All except last 2
nums[:-2]  # [0, 1, 2, 3, 4, 5, 6, 7]
# From start to -2 (NOT included)

# From index 2 to 2nd-to-last
nums[2:-2]  # [2, 3, 4, 5, 6, 7]
# Positive start, negative stop

# Visualization:
#  Positive:  0  1  2  3  4  5  6  7  8  9
#  Values:    0  1  2  3  4  5  6  7  8  9
#  Negative: -10 -9 -8 -7 -6 -5 -4 -3 -2 -1

```
#### Step Parameter [start:stop:step]
```
nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

# Every 2nd item (skip one each time)
nums[::2]  # [0, 2, 4, 6, 8]
# Indices: 0, 2, 4, 6, 8

# Every 3rd item
nums[::3]  # [0, 3, 6, 9]
# Indices: 0, 3, 6, 9

# First 7 items, every 2nd
nums[:7:2]  # [0, 2, 4, 6]
# Indices: 0, 2, 4, 6 (stops before 7)

# From index 1, every 2nd
nums[1::2]  # [1, 3, 5, 7, 9]
# Indices: 1, 3, 5, 7, 9 (odd indices)

```
#### Negative Step (Reverse)
```
nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

# Complete reverse
nums[::-1]  # [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]
# Start at end, go to beginning, step = -1

# Reverse first 5 items
nums[4::-1]  # [4, 3, 2, 1, 0]
# Start at index 4, go backwards to beginning

# Every 2nd item, reversed
nums[::-2]  # [9, 7, 5, 3, 1]
# Start at end, go backwards, skip one each time

# Reverse from index 2 to 7
nums[7:2:-1]  # [7, 6, 5, 4, 3]
# Start at 7, go backwards to 3 (2 NOT included)

```
### Slice Length Formula:
```
# For positive step:
length = (stop - start) // step
# If result is negative or zero, slice is empty

# Examples:
nums[2:7]    # (7-2)//1 = 5 items
nums[2:10:2] # (10-2)//2 = 4 items
nums[:5]     # (5-0)//1 = 5 items

```
### Empty Slices:
```
nums = [0, 1, 2, 3, 4, 5]

# Start >= stop (with positive step)
nums[3:2]   # [] (empty)
nums[5:5]   # [] (empty)
nums[10:20] # [] (empty - both beyond list)

# Step goes wrong direction
nums[2:5:-1]  # [] (can't go forward with negative step)

```
### Slicing Creates Copies:
```
original = [1, 2, 3, 4, 5]

# Create slice (this is a NEW list)
sliced = original[1:4]  # [2, 3, 4]

# Modify the slice
sliced[0] = 99  # sliced = [99, 3, 4]

# Original is unchanged!
print(original)  # [1, 2, 3, 4, 5]

# To copy entire list:
copy1 = original[:]        # Using slice
copy2 = original.copy()    # Using method
copy3 = list(original)     # Using constructor

```
### Common Patterns:
```
# Split list in half
midpoint = len(nums) // 2
first_half = nums[:midpoint]
second_half = nums[midpoint:]

# Get first, middle, last
first = nums[:1]    # or [nums[0]] as list
middle = nums[1:-1]
last = nums[-1:]    # or [nums[-1]] as list

# Alternate items (odd/even indices)
evens = nums[::2]   # Indices 0, 2, 4, 6...
odds = nums[1::2]   # Indices 1, 3, 5, 7...

# Reverse
reversed_list = nums[::-1]

# Remove first and last
inner = nums[1:-1]

```
### Common Mistakes:
#### 1. Forgetting stop is Exclusive
```
# WRONG: Expecting index 5 to be included
nums[2:5]  # Gets indices 2, 3, 4 (NOT 5!)

# CORRECT: To include index 5
nums[2:6]  # Gets indices 2, 3, 4, 5

```
#### 2. Modifying Slice Thinking It Modifies Original
```
# WRONG:
original = [1, 2, 3, 4, 5]
sliced = original[1:3]  # [2, 3]
sliced[0] = 99          # sliced = [99, 3]
# original is still [1, 2, 3, 4, 5] - unchanged!

# To modify original, use slice assignment:
original[1:3] = [99, 99]  # original = [1, 99, 99, 4, 5]

```
#### 3. Confusing Slice vs Index
```
nums = [10, 20, 30, 40]

# Index returns single item
item = nums[2]  # 30 (integer)

# Slice returns list
slice_result = nums[2:3]  # [30] (list with one item)

```
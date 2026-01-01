---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Original list: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J']
Indices:       0    1    2    3    4    5    6    7    8    9

=== Basic Slicing [start:stop] ===

letters[0:3]:   ['A', 'B', 'C']
  (Indices 0, 1, 2 - NOT 3!)

letters[2:6]:   ['C', 'D', 'E', 'F']
  (Indices 2, 3, 4, 5 - NOT 6!)

letters[7:10]:  ['H', 'I', 'J']
  (Indices 7, 8, 9)

=== Omitting Start or Stop ===

letters[:5]:    ['A', 'B', 'C', 'D', 'E']
  (From beginning to index 4)

letters[5:]:    ['F', 'G', 'H', 'I', 'J']
  (From index 5 to end)

letters[:]:     ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J']
  (Complete copy of list)

=== Negative Indices in Slices ===

Original: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J']

letters[-3:]:   ['H', 'I', 'J']
  (Last 3 items)

letters[:-2]:   ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H']
  (Everything except last 2)

letters[2:-2]:  ['C', 'D', 'E', 'F', 'G', 'H']
  (From index 2 to 2nd-to-last)

=== Step Parameter [start:stop:step] ===

Original: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J']

letters[::2]:   ['A', 'C', 'E', 'G', 'I']
  (Indices 0, 2, 4, 6, 8)

letters[::3]:   ['A', 'D', 'G', 'J']
  (Indices 0, 3, 6, 9)

letters[:6:2]:  ['A', 'C', 'E']
  (Indices 0, 2, 4 from first 6)

=== Reversing with Negative Step ===

Original: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J']

letters[::-1]:  ['J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A']
  (Completely reversed)

letters[4::-1]: ['E', 'D', 'C', 'B', 'A']
  (First 5 items reversed)

letters[::-2]:  ['J', 'H', 'F', 'D', 'B']
  (Every 2nd item, going backwards)

=== Slicing Creates a Copy ===

Original: [1, 2, 3, 4, 5]
Slice [1:4]: [2, 3, 4]

After changing slice_copy[0] to 99:
  slice_copy: [99, 3, 4]
  original:   [1, 2, 3, 4, 5]
  (Original unchanged - slice is a copy!)

=== Practical Example: Weekly Data ===

Daily temperatures: [72, 75, 73, 71, 70, 68, 74]

Weekday temps: [72, 75, 73, 71, 70]
Weekend temps: [68, 74]
First half: [72, 75, 73, 71]
Second half: [71, 70, 68, 74]

=== Practical Example: Playlist ===

Full playlist (8 songs):
  1. Bohemian Rhapsody
  2. Stairway to Heaven
  3. Hotel California
  4. Imagine
  5. Sweet Child O' Mine
  6. Purple Haze
  7. Like a Rolling Stone
  8. Smells Like Teen Spirit

Top 3: ['Bohemian Rhapsody', 'Stairway to Heaven', 'Hotel California']
Bottom 2: ['Like a Rolling Stone', 'Smells Like Teen Spirit']
Odd positions: ['Bohemian Rhapsody', 'Hotel California', 'Sweet Child O\' Mine', 'Like a Rolling Stone']

Reversed playlist:
  1. Smells Like Teen Spirit
  2. Like a Rolling Stone
  3. Purple Haze
  4. Sweet Child O' Mine
  5. Imagine
  6. Hotel California
  7. Stairway to Heaven
  8. Bohemian Rhapsody

=== Practical Example: Array Processing ===

Data: [5, 12, 8, 23, 15, 7, 19, 11, 6, 14]

First 3 values: [5, 12, 8]
Average of first 3: 8.3

Last 3 values: [11, 6, 14]
Average of last 3: 10.3

Middle values (exclude first 2 & last 2): [8, 23, 15, 7, 19, 11]
Average of middle: 13.8

Even indices (0,2,4...): [5, 8, 15, 19, 6]
Odd indices (1,3,5...): [12, 23, 7, 11, 14]

=== Slice Length Calculation ===

numbers[2:7]: [2, 3, 4, 5, 6]
Length: 5 = stop - start = 7 - 2 = 5

numbers[:5]: [0, 1, 2, 3, 4]
Length: 5 = stop - start = 5 - 0 = 5

numbers[3:]: [3, 4, 5, 6, 7, 8, 9]
Length: 7 = len(list) - start = 10 - 3 = 7
```

```python
# List Slicing: Extract Portions of Lists

# Sample list for all examples
letters = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"]
print("Original list:", letters)
print(f"Indices:       0    1    2    3    4    5    6    7    8    9")
print()

# ========================================
# BASIC SLICING: [start:stop]
# ========================================

print("=== Basic Slicing [start:stop] ===")
print()

# Get first 3 items [0:3]
first_three = letters[0:3]
print(f"letters[0:3]:   {first_three}")
print("  (Indices 0, 1, 2 - NOT 3!)")
print()

# Get items 2-5 [2:6]
middle = letters[2:6]
print(f"letters[2:6]:   {middle}")
print("  (Indices 2, 3, 4, 5 - NOT 6!)")
print()

# Get last 3 items [7:10]
last_three = letters[7:10]
print(f"letters[7:10]:  {last_three}")
print("  (Indices 7, 8, 9)")
print()

# ========================================
# OMITTING START OR STOP
# ========================================

print("=== Omitting Start or Stop ===")
print()

# Omit start (defaults to 0)
first_five = letters[:5]
print(f"letters[:5]:    {first_five}")
print("  (From beginning to index 4)")
print()

# Omit stop (defaults to end)
from_index_five = letters[5:]
print(f"letters[5:]:    {from_index_five}")
print("  (From index 5 to end)")
print()

# Omit both (copy entire list)
full_copy = letters[:]
print(f"letters[:]:     {full_copy}")
print("  (Complete copy of list)")
print()

# ========================================
# NEGATIVE INDICES IN SLICES
# ========================================

print("=== Negative Indices in Slices ===")
print()

print(f"Original: {letters}")
print()

# Last 3 items using negative index
last_three_neg = letters[-3:]
print(f"letters[-3:]:   {last_three_neg}")
print("  (Last 3 items)")
print()

# All except last 2
all_but_last_two = letters[:-2]
print(f"letters[:-2]:   {all_but_last_two}")
print("  (Everything except last 2)")
print()

# From index 2 to 2nd-to-last
middle_portion = letters[2:-2]
print(f"letters[2:-2]:  {middle_portion}")
print("  (From index 2 to 2nd-to-last)")
print()

# ========================================
# STEP PARAMETER [start:stop:step]
# ========================================

print("=== Step Parameter [start:stop:step] ===")
print()

print(f"Original: {letters}")
print()

# Every 2nd item (skip one each time)
every_second = letters[::2]
print(f"letters[::2]:   {every_second}")
print("  (Indices 0, 2, 4, 6, 8)")
print()

# Every 3rd item
every_third = letters[::3]
print(f"letters[::3]:   {every_third}")
print("  (Indices 0, 3, 6, 9)")
print()

# First 6 items, every 2nd
first_six_alternating = letters[:6:2]
print(f"letters[:6:2]:  {first_six_alternating}")
print("  (Indices 0, 2, 4 from first 6)")
print()

# ========================================
# REVERSING WITH NEGATIVE STEP
# ========================================

print("=== Reversing with Negative Step ===")
print()

print(f"Original: {letters}")
print()

# Reverse entire list
reversed_list = letters[::-1]
print(f"letters[::-1]:  {reversed_list}")
print("  (Completely reversed)")
print()

# Reverse first 5 items
reversed_first_five = letters[4::-1]
print(f"letters[4::-1]: {reversed_first_five}")
print("  (First 5 items reversed)")
print()

# Every 2nd item, reversed
every_second_reversed = letters[::-2]
print(f"letters[::-2]:  {every_second_reversed}")
print("  (Every 2nd item, going backwards)")
print()

# ========================================
# SLICING CREATES A COPY
# ========================================

print("=== Slicing Creates a Copy ===")
print()

original = [1, 2, 3, 4, 5]
print(f"Original: {original}")

# Create slice
slice_copy = original[1:4]
print(f"Slice [1:4]: {slice_copy}")

# Modify the slice
slice_copy[0] = 99
print(f"\nAfter changing slice_copy[0] to 99:")
print(f"  slice_copy: {slice_copy}")
print(f"  original:   {original}")
print("  (Original unchanged - slice is a copy!)")

print()

# ========================================
# PRACTICAL EXAMPLES
# ========================================

print("=== Practical Example: Weekly Data ===")
print()

temperatures = [72, 75, 73, 71, 70, 68, 74]
days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"]

print("Daily temperatures:", temperatures)
print()

# Weekday temperatures (Mon-Fri)
weekdays = temperatures[:5]
print(f"Weekday temps: {weekdays}")

# Weekend temperatures (Sat-Sun)
weekend = temperatures[-2:]
print(f"Weekend temps: {weekend}")

# First half of week
first_half = temperatures[:4]
print(f"First half: {first_half}")

# Second half of week
second_half = temperatures[3:]
print(f"Second half: {second_half}")

print()

print("=== Practical Example: Playlist ===")
print()

songs = [
    "Bohemian Rhapsody",
    "Stairway to Heaven",
    "Hotel California",
    "Imagine",
    "Sweet Child O' Mine",
    "Purple Haze",
    "Like a Rolling Stone",
    "Smells Like Teen Spirit"
]

print(f"Full playlist ({len(songs)} songs):")
for i, song in enumerate(songs, start=1):
    print(f"  {i}. {song}")

print()

# Top 3 songs
top_three = songs[:3]
print(f"Top 3: {top_three}")

# Bottom 2 songs
bottom_two = songs[-2:]
print(f"Bottom 2: {bottom_two}")

# Every other song (odd positions)
odd_songs = songs[::2]
print(f"Odd positions: {odd_songs}")

# Reverse playlist
reversed_playlist = songs[::-1]
print(f"\nReversed playlist:")
for i, song in enumerate(reversed_playlist, start=1):
    print(f"  {i}. {song}")

print()

print("=== Practical Example: Array Processing ===")
print()

data = [5, 12, 8, 23, 15, 7, 19, 11, 6, 14]
print(f"Data: {data}")
print()

# Get first 3 values
first_3 = data[:3]
print(f"First 3 values: {first_3}")
print(f"Average of first 3: {sum(first_3) / len(first_3):.1f}")

print()

# Get last 3 values
last_3 = data[-3:]
print(f"Last 3 values: {last_3}")
print(f"Average of last 3: {sum(last_3) / len(last_3):.1f}")

print()

# Get middle values (exclude first 2 and last 2)
middle_values = data[2:-2]
print(f"Middle values (exclude first 2 & last 2): {middle_values}")
print(f"Average of middle: {sum(middle_values) / len(middle_values):.1f}")

print()

# Even-indexed items
even_indices = data[::2]
print(f"Even indices (0,2,4...): {even_indices}")

# Odd-indexed items
odd_indices = data[1::2]
print(f"Odd indices (1,3,5...): {odd_indices}")

print()

# ========================================
# SLICE LENGTH CALCULATION
# ========================================

print("=== Slice Length Calculation ===")
print()

numbers = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

slice1 = numbers[2:7]
print(f"numbers[2:7]: {slice1}")
print(f"Length: {len(slice1)} = stop - start = 7 - 2 = 5")

print()

slice2 = numbers[:5]
print(f"numbers[:5]: {slice2}")
print(f"Length: {len(slice2)} = stop - start = 5 - 0 = 5")

print()

slice3 = numbers[3:]
print(f"numbers[3:]: {slice3}")
print(f"Length: {len(slice3)} = len(list) - start = 10 - 3 = 7")
```

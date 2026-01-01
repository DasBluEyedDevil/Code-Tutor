---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Rectangle Pattern (3x5) ===
*****
*****
*****

=== Right Triangle ===
*
**
***
****
*****

=== Multiplication Table (1-5) ===
1x1= 1  1x2= 2  1x3= 3  1x4= 4  1x5= 5  1x6= 6  1x7= 7  1x8= 8  1x9= 9  1x10=10  
2x1= 2  2x2= 4  2x3= 6  2x4= 8  2x5=10  2x6=12  2x7=14  2x8=16  2x9=18  2x10=20  
3x1= 3  3x2= 6  3x3= 9  3x4=12  3x5=15  3x6=18  3x7=21  3x8=24  3x9=27  3x10=30  
4x1= 4  4x2= 8  4x3=12  4x4=16  4x5=20  4x6=24  4x7=28  4x8=32  4x9=36  4x10=40  
5x1= 5  5x2=10  5x3=15  5x4=20  5x5=25  5x6=30  5x7=35  5x8=40  5x9=45  5x10=50  

=== Coordinate Grid (3x3) ===
(1,1) (1,2) (1,3) 
(2,1) (2,2) (2,3) 
(3,1) (3,2) (3,3) 

=== Inverted Triangle ===
*****
****
***
**
*

=== Numbered Grid ===
 1  2  3 
 4  5  6 
 7  8  9 
10 11 12 

=== Pyramid Pattern ===
    *
   ***
  *****
 *******
*********

=== break Only Affects Inner Loop ===
Outer loop: 0
  Inner: 0
  Inner: 1
  Breaking inner at 2
  Outer continues...
Outer loop: 1
  Inner: 0
  Inner: 1
  Breaking inner at 2
  Outer continues...
Outer loop: 2
  Inner: 0
  Inner: 1
  Breaking inner at 2
  Outer continues...

=== Full Times Table ===
       1   2   3   4   5   6   7   8   9  10
    ----------------------------------------
 1 |   1   2   3   4   5   6   7   8   9  10
 2 |   2   4   6   8  10  12  14  16  18  20
 3 |   3   6   9  12  15  18  21  24  27  30
 4 |   4   8  12  16  20  24  28  32  36  40
 5 |   5  10  15  20  25  30  35  40  45  50
 6 |   6  12  18  24  30  36  42  48  54  60
 7 |   7  14  21  28  35  42  49  56  63  70
 8 |   8  16  24  32  40  48  56  64  72  80
 9 |   9  18  27  36  45  54  63  72  81  90
10 |  10  20  30  40  50  60  70  80  90 100

=== Iterating 2D List ===
Row 1, Seat 1: Alice
Row 1, Seat 2: Bob
Row 1, Seat 3: Charlie
Row 2, Seat 1: David
Row 2, Seat 2: Eve
Row 2, Seat 3: Frank
Row 3, Seat 1: Grace
Row 3, Seat 2: Henry
Row 3, Seat 3: Iris
```

```python
# Nested Loops: Loops Within Loops

# Example 1: Basic Nested Loop (Rectangle)
print("=== Rectangle Pattern (3x5) ===")

for row in range(3):  # Outer loop: 3 rows
    for col in range(5):  # Inner loop: 5 columns per row
        print("*", end="")  # Print star without newline
    print()  # Newline after each row

# Output:
# *****
# *****
# *****

print()

# Example 2: Right Triangle (Growing Rows)
print("=== Right Triangle ===")

for row in range(1, 6):  # Rows: 1, 2, 3, 4, 5
    for col in range(row):  # Columns: matches row number
        print("*", end="")
    print()

# Output:
# *
# **
# ***
# ****
# *****

print()

# Example 3: Multiplication Table
print("=== Multiplication Table (1-5) ===")

for num in range(1, 6):  # Numbers 1-5
    for mult in range(1, 11):  # Multipliers 1-10
        result = num * mult
        print(f"{num}x{mult}={result:2}", end="  ")  # :2 pads to 2 digits
    print()  # Newline after each number's row

print()

# Example 4: Coordinates Grid
print("=== Coordinate Grid (3x3) ===")

for x in range(1, 4):  # x coordinates: 1, 2, 3
    for y in range(1, 4):  # y coordinates: 1, 2, 3
        print(f"({x},{y})", end=" ")
    print()

# Output:
# (1,1) (1,2) (1,3)
# (2,1) (2,2) (2,3)
# (3,1) (3,2) (3,3)

print()

# Example 5: Inverted Triangle
print("=== Inverted Triangle ===")

for row in range(5, 0, -1):  # 5, 4, 3, 2, 1 (countdown)
    for col in range(row):
        print("*", end="")
    print()

# Output:
# *****
# ****
# ***
# **
# *

print()

# Example 6: Numbered Grid
print("=== Numbered Grid ===")

number = 1

for row in range(4):  # 4 rows
    for col in range(3):  # 3 columns
        print(f"{number:2}", end=" ")  # Print with padding
        number = number + 1
    print()

# Output:
#  1  2  3
#  4  5  6
#  7  8  9
# 10 11 12

print()

# Example 7: Pyramid Pattern (Centered)
print("=== Pyramid Pattern ===")

height = 5

for row in range(1, height + 1):
    # Print leading spaces
    for space in range(height - row):
        print(" ", end="")
    
    # Print stars
    for star in range(2 * row - 1):
        print("*", end="")
    
    print()  # Newline

# Output:
#     *
#    ***
#   *****
#  *******
# *********

print()

# Example 8: break/continue in Nested Loops
print("=== break Only Affects Inner Loop ===")

for outer in range(3):
    print(f"Outer loop: {outer}")
    
    for inner in range(5):
        if inner == 2:
            print(f"  Breaking inner at {inner}")
            break  # Only exits INNER loop
        print(f"  Inner: {inner}")
    
    print("  Outer continues...")  # This still runs!

print()

# Example 9: Times Table (Full 10x10)
print("=== Full Times Table ===")

# Header row
print("    ", end="")
for i in range(1, 11):
    print(f"{i:4}", end="")
print()
print("    " + "-" * 40)

# Table rows
for row in range(1, 11):
    print(f"{row:2} |", end="")
    for col in range(1, 11):
        product = row * col
        print(f"{product:4}", end="")
    print()

print()

# Example 10: Nested List Iteration (Preview)
print("=== Iterating 2D List ===")

classroom = [
    ["Alice", "Bob", "Charlie"],
    ["David", "Eve", "Frank"],
    ["Grace", "Henry", "Iris"]
]

for row_num, row in enumerate(classroom):
    for seat_num, student in enumerate(row):
        print(f"Row {row_num + 1}, Seat {seat_num + 1}: {student}")
```

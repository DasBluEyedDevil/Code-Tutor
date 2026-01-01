---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Counting 1 to 5 ===
Number: 1
Number: 2
Number: 3
Number: 4
Number: 5
Done!

=== range(5) - Starts at 0 ===
0
1
2
3
4

=== Even Numbers (Step by 2) ===
0
2
4
6
8
10

=== Iterating Over String ===
Letter: P
Letter: y
Letter: t
Letter: h
Letter: o
Letter: n

=== Sum of 1 to 10 ===
Adding 1 to total (0)
Adding 2 to total (1)
Adding 3 to total (3)
Adding 4 to total (6)
Adding 5 to total (10)
Adding 6 to total (15)
Adding 7 to total (21)
Adding 8 to total (28)
Adding 9 to total (36)
Adding 10 to total (45)
Final sum: 55

=== Building Stars ===
*
**
***
****
*****

=== Multiplication Table (5s) ===
5 × 1 = 5
5 × 2 = 10
5 × 3 = 15
5 × 4 = 20
5 × 5 = 25
5 × 6 = 30
5 × 7 = 35
5 × 8 = 40
5 × 9 = 45
5 × 10 = 50

=== Countdown ===
5
4
3
2
1
Blastoff! 🚀

=== Simple Pattern ===
*****
*****
*****

=== Odd Numbers Only (1-10) ===
1
3
5
7
9
```

```python
# for Loops: Iterating Over Sequences

# Example 1: Basic for Loop with range()
print("=== Counting 1 to 5 ===")

for number in range(1, 6):  # range(1, 6) generates: 1, 2, 3, 4, 5
    print(f"Number: {number}")

print("Done!")
print()

# Example 2: range() with One Argument (starts at 0)
print("=== range(5) - Starts at 0 ===")

for i in range(5):  # range(5) generates: 0, 1, 2, 3, 4
    print(i)

print()

# Example 3: range() with Step (count by 2s)
print("=== Even Numbers (Step by 2) ===")

for num in range(0, 11, 2):  # Start at 0, stop before 11, step by 2
    print(num)  # Outputs: 0, 2, 4, 6, 8, 10

print()

# Example 4: Iterating Over a String
print("=== Iterating Over String ===")

name = "Python"

for letter in name:
    print(f"Letter: {letter}")
# Outputs each letter: P, y, t, h, o, n

print()

# Example 5: Accumulator Pattern (Sum)
print("=== Sum of 1 to 10 ===")

total = 0

for number in range(1, 11):  # 1 through 10
    print(f"Adding {number} to total ({total})")
    total = total + number

print(f"Final sum: {total}")  # 55
print()

# Example 6: Building a String
print("=== Building Stars ===")

stars = ""

for i in range(1, 6):
    stars = stars + "*"
    print(stars)
# Outputs:
# *
# **
# ***
# ****
# *****

print()

# Example 7: Multiplication Table
print("=== Multiplication Table (5s) ===")

for i in range(1, 11):
    result = 5 * i
    print(f"5 × {i} = {result}")

print()

# Example 8: Counting Backwards (Negative Step)
print("=== Countdown ===")

for count in range(5, 0, -1):  # Start at 5, stop before 0, step -1
    print(count)

print("Blastoff! 🚀")
print()

# Example 9: Nested Loop Preview (Patterns)
print("=== Simple Pattern ===")

for row in range(3):  # 3 rows
    for col in range(5):  # 5 stars per row
        print("*", end="")  # end="" prevents newline
    print()  # Move to next line after each row

# Outputs:
# *****
# *****
# *****

print()

# Example 10: Skipping Values with Continue (Preview)
print("=== Odd Numbers Only (1-10) ===")

for num in range(1, 11):
    if num % 2 == 0:  # If even
        continue  # Skip to next iteration
    print(num)  # Only prints odd numbers

# Outputs: 1, 3, 5, 7, 9
```

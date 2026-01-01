---
type: "THEORY"
title: "Syntax Breakdown"
---

### Nested Loop Anatomy:
```
for outer_var in outer_sequence:    # Outer loop
    # Outer loop body
    
    for inner_var in inner_sequence:  # Inner loop
        # Inner loop body
        # Can use both outer_var and inner_var here
    
    # Back to outer loop

```
#### Execution Flow:

- Outer loop starts (first iteration)
- Inner loop runs completely (all its iterations)
- Outer loop continues (second iteration)
- Inner loop runs completely again
- Repeat until outer loop finishes

#### Visual Example:
```
for row in range(3):      # Outer: rows
    for col in range(2):  # Inner: columns
        print("*", end="")
    print()  # Newline

# Execution trace:
# row=0: col=0 (*), col=1 (*), newline
# row=1: col=0 (*), col=1 (*), newline
# row=2: col=0 (*), col=1 (*), newline

# Output:
# **
# **
# **

```
### Total Iterations:
```
Outer iterations × Inner iterations = Total

for i in range(3):    # Runs 3 times
    for j in range(4):  # Runs 4 times per outer
        print("X")

# Total: 3 × 4 = 12 times "X" is printed

```
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Outer (i)</th><th>Inner (j) iterations</th><th>Total so far</th></tr><tr><td>0</td><td>0, 1, 2, 3</td><td>4</td></tr><tr><td>1</td><td>0, 1, 2, 3</td><td>8</td></tr><tr><td>2</td><td>0, 1, 2, 3</td><td>12</td></tr></table>### Common Patterns:
#### 1. Rectangle/Grid (Fixed Dimensions)
```
for row in range(rows):    # Fixed number of rows
    for col in range(cols):  # Fixed number per row
        print("*", end="")
    print()

```
#### 2. Right Triangle (Growing)
```
for row in range(1, n+1):  # Row numbers
    for col in range(row):   # Number of cols = row number
        print("*", end="")
    print()

```
#### 3. Inverted Triangle (Shrinking)
```
for row in range(n, 0, -1):  # Countdown
    for col in range(row):     # Decreasing columns
        print("*", end="")
    print()

```
#### 4. Multiplication Table
```
for num in range(1, 11):     # Numbers 1-10
    for mult in range(1, 11):  # Times 1-10
        print(num * mult, end=" ")
    print()

```
#### 5. Coordinate Grid
```
for x in range(width):
    for y in range(height):
        process_cell(x, y)

```
### break and continue in Nested Loops:
**Critical:** break/continue only affect the innermost loop they're in!

```
for outer in range(3):
    print(f"Outer: {outer}")
    
    for inner in range(5):
        if inner == 2:
            break  # Only exits THIS (inner) loop
        print(f"  Inner: {inner}")
    
    # Outer loop continues!
    print("  Back to outer")

# Output:
# Outer: 0
#   Inner: 0
#   Inner: 1
#   Back to outer
# Outer: 1
#   Inner: 0
#   Inner: 1
#   Back to outer
# Outer: 2
#   Inner: 0
#   Inner: 1
#   Back to outer

```
To break out of multiple loops, you need a different strategy:

```
# Method 1: Flag variable
break_outer = False

for outer in range(10):
    for inner in range(10):
        if condition:
            break_outer = True
            break
    if break_outer:
        break

# Method 2: Function with return (better!)
def search_2d():
    for outer in range(10):
        for inner in range(10):
            if found_it:
                return  # Exits function (and all loops)

```
### Performance Considerations:
Nested loops can get expensive quickly:

```
for i in range(100):      # 100 times
    for j in range(100):  # × 100 times
        # Total: 10,000 iterations!

for i in range(1000):     # 1,000 times
    for j in range(1000): # × 1,000 times
        # Total: 1,000,000 iterations!

```
**Big O Notation (preview):**

- Single loop: O(n) - linear time
- Nested loop: O(n²) - quadratic time (much slower!)
- Triple nested: O(n³) - cubic time (very slow!)

**Best practices:**

- Minimize nesting depth (2-3 levels max)
- Use break to exit early when possible
- Consider alternatives for very large datasets

### Common Mistakes:

<li>**Forgetting newline after inner loop**:```
# WRONG: Everything on one line
for row in range(3):
    for col in range(5):
        print("*", end="")
# Output: *************** (all one line!)

# CORRECT: Newline after each row
for row in range(3):
    for col in range(5):
        print("*", end="")
    print()  # Newline after inner loop

```
</li><li>**Using wrong variable in inner loop**:```
# WRONG:
for row in range(5):
    for col in range(row):  # Uses 'row', not a fixed number
        print(row, end="")  # Prints row number, not col!

# If you want a square, use a fixed number:
for row in range(5):
    for col in range(5):  # Fixed 5 columns
        print("*", end="")

```
</li><li>**Confusing indentation**:```
# WRONG: print() indented inside inner loop
for row in range(3):
    for col in range(5):
        print("*", end="")
        print()  # Newline after EACH star!
# Output: *
#         *
#         * (each star on new line)

# CORRECT: print() at outer loop level
for row in range(3):
    for col in range(5):
        print("*", end="")
    print()  # Newline after each ROW

```
</li>
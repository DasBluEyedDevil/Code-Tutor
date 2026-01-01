---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Wrong Newline Placement**
```python
# WRONG - Newline inside inner loop (one star per line!)
for row in range(3):
    for col in range(5):
        print("*", end="")
        print()  # Every star gets a newline!

# CORRECT - Newline after inner loop completes
for row in range(3):
    for col in range(5):
        print("*", end="")
    print()  # Newline after each ROW
```

**2. break Only Exits Innermost Loop**
```python
# WRONG expectation - break exits BOTH loops
for row in range(5):
    for col in range(5):
        if found_target:
            break  # Only exits inner loop!
    # Outer loop continues!

# To exit both loops, use a flag:
found = False
for row in range(5):
    for col in range(5):
        if target:
            found = True
            break
    if found:
        break  # Exit outer too
```

**3. Using Wrong Loop Variable**
```python
# WRONG - Prints row number instead of asterisks
for row in range(3):
    for col in range(row):  # Depends on row
        print(row, end="")  # Printing row, not col!

# Understand what each variable represents
for row in range(3):      # row = 0, 1, 2
    for col in range(5):  # col = 0, 1, 2, 3, 4 for EACH row
        print(col, end=" ")  # Use the right variable
```

**4. Performance with Large Ranges**
```python
# SLOW - O(n²) = 1,000,000 iterations!
for i in range(1000):
    for j in range(1000):
        process(i, j)  # Runs 1 million times!

# Nested loops multiply:
# 10 × 10 = 100 iterations
# 100 × 100 = 10,000 iterations
# 1000 × 1000 = 1,000,000 iterations!
```

**5. Forgetting end="" for Same-Line Output**
```python
# WRONG - Each star on new line
for row in range(3):
    for col in range(5):
        print("*")  # No end="", creates newlines

# CORRECT - Stars on same line
for row in range(3):
    for col in range(5):
        print("*", end="")  # Stay on same line
    print()  # New line after row
```
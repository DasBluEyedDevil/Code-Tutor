---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Total fruit: 8
Money left: $38
Total cookies: 48
Each person gets: 3.3333333333333335 slices
Whole slices each: 3
Leftover slices: 1
5 squared: 25
2 cubed: 8
```

```python
# The 7 Basic Math Operators in Python

# 1. Addition (+): Combine numbers
apples = 5
oranges = 3
total_fruit = apples + oranges
print(f"Total fruit: {total_fruit}")  # Output: 8

# 2. Subtraction (-): Take away
money = 50
price = 12
left_over = money - price
print(f"Money left: ${left_over}")  # Output: $38

# 3. Multiplication (*): Repeat a value
cookies_per_box = 12
boxes = 4
total_cookies = cookies_per_box * boxes
print(f"Total cookies: {total_cookies}")  # Output: 48

# 4. Division (/): Split into parts (always gives decimal)
pizza_slices = 10
people = 3
slices_each = pizza_slices / people
print(f"Each person gets: {slices_each} slices")  # Output: 3.3333...

# 5. Floor Division (//): Whole parts only (no decimals)
slices_each_whole = pizza_slices // people
print(f"Whole slices each: {slices_each_whole}")  # Output: 3

# 6. Modulo (%): The remainder (what's left over)
leftover_slices = pizza_slices % people
print(f"Leftover slices: {leftover_slices}")  # Output: 1

# 7. Exponentiation (**): Power/repeated multiplication
square = 5 ** 2  # 5 × 5
print(f"5 squared: {square}")  # Output: 25

cube = 2 ** 3  # 2 × 2 × 2
print(f"2 cubed: {cube}")  # Output: 8
```
---
type: "THEORY"
title: "Syntax Breakdown"
---

### The Three Logical Operators:
#### 1. AND Operator - All Must Be True
```python
condition1 and condition2 and condition3
```

**Truth table for AND:**

| Condition 1 | Condition 2 | Result |
| :--- | :--- | :--- |
| True | True | **True** |
| True | False | False |
| False | True | False |
| False | False | False |

**Memory trick:** Think of AND as a strict gatekeeper - everything must be true!

```python
# Real examples:
age >= 18 and has_license  # Both must be True to drive
username_correct and password_correct  # Both must be True to log in
in_stock and payment_valid  # Both must be True to buy
```

#### 2. OR Operator - At Least One Must Be True
```python
condition1 or condition2 or condition3
```

**Truth table for OR:**

| Condition 1 | Condition 2 | Result |
| :--- | :--- | :--- |
| True | True | **True** |
| True | False | **True** |
| False | True | **True** |
| False | False | False |

**Memory trick:** Think of OR as a lenient gatekeeper - any reason works!

```python
# Real examples:
is_weekend or is_holiday  # Either one means no work!
is_admin or is_owner  # Either one has full access
paid_with_card or paid_with_cash  # Either payment method works
```

#### 3. NOT Operator - Reverses True/False
```python
not condition
```

**Truth table for NOT:**

| Condition | Result |
| :--- | :--- |
| True | **False** |
| False | **True** |

**Memory trick:** NOT flips the switch!

```python
# Real examples:
not is_logged_in  # True if NOT logged in (need to show login page)
not is_empty  # True if NOT empty (has content)
not game_over  # True if NOT game over (keep playing)
```

#### 4. Combining Multiple Operators
```python
# Use parentheses to control order!
(condition1 or condition2) and condition3
```

**Without parentheses:** `and` has higher precedence than `or`

```python
# This:
True or False and False
# Is evaluated as:
True or (False and False)  # Result: True

# To change order, use parentheses:
(True or False) and False  # Result: False
```

**Best practice:** Always use parentheses to make your intent crystal clear!

#### 5. Short-Circuit Evaluation (Efficiency Bonus)
Python is smart - it stops checking as soon as it knows the answer:

```python
# AND short-circuit:
False and (anything)  # Python stops at False (result is already False)

# OR short-circuit:
True or (anything)  # Python stops at True (result is already True)
```

**Why this matters:**

```python
# Safe division check
if denominator != 0 and numerator / denominator > 10:
    # If denominator is 0, Python stops at first condition
    # Never attempts the division (which would crash!)
```

#### 6. Common Patterns and Mistakes
✅ **Correct:**

```python
if age >= 18 and age <= 65:
    print("Working age")

# Even better - chained comparison:
if 18 <= age <= 65:
    print("Working age")
```

❌ **Wrong - Comparing to multiple values:**

```python
# WRONG: This doesn't work like English!
if age == 18 or 21 or 25:  # Doesn't check if age is 18, 21, or 25!

# CORRECT:
if age == 18 or age == 21 or age == 25:
    print("Special age")

# OR use 'in' with a tuple:
if age in (18, 21, 25):
    print("Special age")
```

❌ **Wrong - Redundant comparisons:**

```python
# WRONG: Checking if bool == True is redundant
if has_permission == True:

# CORRECT: Bools are already True/False
if has_permission:

# For NOT:
if has_permission == False:  # Redundant
if not has_permission:       # Better
```

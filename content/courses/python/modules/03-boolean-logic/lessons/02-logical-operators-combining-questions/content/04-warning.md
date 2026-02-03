---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. The OR Trap - Comparing to Multiple Values Wrong**
```python
# WRONG - This doesn't do what you think!
if age == 18 or 21 or 25:  # Always True! (21 is truthy)
    print("Special age")

# CORRECT - Compare age to each value
if age == 18 or age == 21 or age == 25:
    print("Special age")

# BETTER - Use 'in' operator
if age in (18, 21, 25):
    print("Special age")
```

**2. Redundant Boolean Comparisons**
```python
# WRONG - Don't compare booleans to True/False
if is_valid == True:   # Redundant!
if is_valid == False:  # Redundant!

# CORRECT - Booleans are already True/False
if is_valid:      # Clean and Pythonic
if not is_valid:  # Clean and Pythonic
```

**3. Forgetting Operator Precedence**
```python
# CONFUSING - What does this mean?
if a or b and c:
    # Is it (a or b) and c?
    # Or a or (b and c)?  # <- This one! 'and' binds tighter

# CLEAR - Use parentheses!
if (a or b) and c:  # Now intent is obvious
```

**4. Logical Operators with Non-Booleans**
```python
# and/or return actual values, not just True/False!
name = "" or "Anonymous"  # Returns "Anonymous" (first truthy)
value = 0 and 100         # Returns 0 (first falsy)

# This is useful but can be surprising if unexpected
```

**5. Negating Complex Conditions (De Morgan's Laws)**
```python
# WRONG mental model:
not (a and b)  # Is NOT the same as (not a and not b)!

# CORRECT (De Morgan's Laws):
not (a and b) == (not a or not b)   # True
not (a or b) == (not a and not b)   # True
```

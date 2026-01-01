---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using 'else if' Instead of 'elif'**
```python
# WRONG - Python uses elif, not else if!
if score >= 90:
    print("A")
else if score >= 80:  # SyntaxError!
    print("B")

# CORRECT
if score >= 90:
    print("A")
elif score >= 80:  # elif is one word!
    print("B")
```

**2. Wrong Order - General Before Specific**
```python
# WRONG - More general condition catches everything!
if score >= 60:
    print("Passing")  # 95 matches here first!
elif score >= 90:
    print("A")  # Never reached!

# CORRECT - Most specific first
if score >= 90:
    print("A")
elif score >= 60:
    print("Passing")
```

**3. Putting else Before elif**
```python
# WRONG - else must be last!
if score >= 90:
    print("A")
else:
    print("Not A")
elif score >= 80:  # SyntaxError: elif after else
    print("B")

# CORRECT - Order: if -> elif -> else
if score >= 90:
    print("A")
elif score >= 80:
    print("B")
else:
    print("Below B")
```

**4. Missing else for Edge Cases**
```python
# RISKY - What if age is negative?
if age < 13:
    price = 8
elif age < 65:
    price = 15
elif age >= 65:
    price = 10
# price undefined if age < 0!

# SAFER - else catches unexpected values
if age < 0:
    print("Invalid age")
elif age < 13:
    price = 8
# ... etc
```

**5. Redundant Conditions in elif**
```python
# REDUNDANT - The < 90 is implied
if score >= 90:
    grade = "A"
elif score >= 80 and score < 90:  # Unnecessary!
    grade = "B"

# CLEAN - elif already means "if was False"
if score >= 90:
    grade = "A"
elif score >= 80:  # Automatically means < 90
    grade = "B"
```
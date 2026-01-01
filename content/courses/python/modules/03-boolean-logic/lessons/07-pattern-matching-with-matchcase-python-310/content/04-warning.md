---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Wildcard First (Catches Everything!)**
```python
# WRONG - case _ catches ALL values!
match status:
    case _:  # Matches everything - others never reached!
        print("Unknown")
    case 200:  # Never executed!
        print("OK")

# CORRECT - Put wildcard last
match status:
    case 200:
        print("OK")
    case _:  # Catches remaining values
        print("Unknown")
```

**2. Python Version Compatibility**
```python
# match/case requires Python 3.10+!
# This will fail on Python 3.9 and earlier

# Check your version first:
import sys
print(sys.version)  # Must be 3.10 or higher

# For older Python, use if/elif:
if status == 200:
    print("OK")
elif status == 404:
    print("Not Found")
```

**3. Confusing Capture vs Literal**
```python
# WRONG - x is a capture variable, not literal!
match value:
    case x:  # Captures ANY value into x
        print(f"Got {x}")
    case 5:  # Never reached - x matched everything!
        print("Five")

# CORRECT - Use literal or guard
match value:
    case 5:
        print("Five")
    case x:  # Now only captures non-5 values
        print(f"Got {x}")
```

**4. OR Pattern Syntax**
```python
# WRONG - Can't use 'or' keyword!
match day:
    case "Sat" or "Sun":  # SyntaxError!
        print("Weekend")

# CORRECT - Use | for OR patterns
match day:
    case "Sat" | "Sun":  # Correct!
        print("Weekend")
```

**5. Guards Must Use Captured Variables**
```python
# WRONG - Can't use external variables directly in guard
threshold = 50
match score:
    case if score > threshold:  # SyntaxError!
        print("High")

# CORRECT - Capture first, then guard
match score:
    case n if n > threshold:  # Capture as n, then use in guard
        print("High")
```
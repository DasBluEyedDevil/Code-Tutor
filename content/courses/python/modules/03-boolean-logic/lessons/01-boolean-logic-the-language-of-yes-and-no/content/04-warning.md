---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using = Instead of == for Comparison**
```python
# WRONG - This is assignment, not comparison!
if age = 18:  # SyntaxError!

# CORRECT - Use == to compare
if age == 18:  # Checks if age equals 18
```

**2. Forgetting Case Sensitivity**
```python
True   # ✅ Correct
true   # ❌ NameError: 'true' is not defined
False  # ✅ Correct
false  # ❌ NameError: 'false' is not defined
```

**3. Case-Sensitive String Comparisons**
```python
name = "Alice"
name == "alice"  # False! Capital A ≠ lowercase a
name.lower() == "alice"  # True - convert to same case first
```

**4. Floating-Point Comparison Gotcha**
```python
0.1 + 0.2 == 0.3  # False! (Due to floating-point representation)
# Use math.isclose() for float comparisons in Python 3.5+
import math
math.isclose(0.1 + 0.2, 0.3)  # True
```

**5. Comparing Different Types**
```python
"5" == 5   # False - string is not equal to integer
int("5") == 5  # True - convert string to int first
```
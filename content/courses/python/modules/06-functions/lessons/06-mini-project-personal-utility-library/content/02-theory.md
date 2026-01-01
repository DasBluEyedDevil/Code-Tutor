---
type: "THEORY"
title: "Designing Good Functions"
---

**Before writing a function, ask yourself:**

1. **What does it do?** (One clear purpose)
2. **What does it need?** (Parameters)
3. **What does it give back?** (Return value)
4. **What could go wrong?** (Edge cases)

**Example thought process:**

```
Function: calculate_percentage
Purpose: Calculate what percentage one number is of another
Needs: part (number), whole (number)
Returns: percentage as a float
Edge cases: whole = 0 (division by zero!), negative numbers
```

**Turns into:**

```python
def calculate_percentage(part, whole, decimal_places=2):
    """Calculate what percentage 'part' is of 'whole'."""
    if whole == 0:
        return 0.0  # Avoid division by zero
    percentage = (part / whole) * 100
    return round(percentage, decimal_places)
```

**Notice:**
- Clear name describes what it does
- Docstring explains the purpose
- Default parameter for common use case
- Handles the edge case (division by zero)
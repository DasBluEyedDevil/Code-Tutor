---
type: "THEORY"
title: "Syntax Breakdown"
---

### elif Chain Anatomy

```python
if condition1:
    # Block 1
elif condition2:
    # Block 2
elif condition3:
    # Block 3
else:
    # Block 4 (optional)
```

#### Breaking It Down:

- **if**: Always comes first
  ```python
  if score >= 90:  # First condition to check
  ```

- **elif**: Short for "else if" - adds more conditions
  ```python
  elif score >= 80:  # Only checked if 'if' was False
  elif score >= 70:  # Only checked if above were False
  # Can have as many elif as needed!
  ```

- **else**: Optional catch-all at the end
  ```python
  else:  # Runs if ALL above were False
  ```

### How Python Reads elif Chains

1. **Start at the top**: Check `if` condition.
2. **If True**: Execute that block, **skip all remaining** elif/else.
3. **If False**: Move to next `elif`, check its condition.
4. **If no conditions match**: Execute `else` block (if present).
5. **Continue**: Code after the entire chain runs.

#### Visual Flow Example:

```python
score = 75

if score >= 90:      # False (75 < 90) -> Check next
    print("A")
elif score >= 80:    # False (75 < 80) -> Check next
    print("B")
elif score >= 70:    # True! (75 >= 70) -> Execute & STOP
    print("C")
elif score >= 60:    # SKIPPED (already found match)
    print("D")
else:                # SKIPPED (already found match)
    print("F")

# Output: C
```

### Order Matters! (Critical Concept)

**Rule:** Put **most specific conditions first**, general ones last.

#### ✅ Correct Order (Specific to General):

```python
if score >= 90:    # Most restrictive
    print("A")
elif score >= 80:  # Less restrictive
    print("B")
elif score >= 70:  # Even less
    print("C")
else:              # Catches everything else
    print("F")
# Works correctly!
```

#### ❌ Wrong Order (General First):

```python
if score >= 60:    # Too general! Matches 60-100!
    print("D")     # This catches EVERYTHING >= 60
elif score >= 70:  # NEVER RUNS (already matched above)
    print("C")
elif score >= 80:  # NEVER RUNS
    print("B")
elif score >= 90:  # NEVER RUNS
    print("A")

# If score = 95:
# Output: D (WRONG! Should be A)
# The 60+ check matched first and stopped
```

### elif vs Multiple if Statements

| elif Chain | Multiple if |
| :--- | :--- |
| Mutually exclusive (only one runs) | Independent checks (multiple can run) |
| Stops at first match | Checks every condition |
| More efficient | Less efficient |
| Use for categories | Use for independent flags |

#### Example Comparison:

```python
# elif chain (MUTUALLY EXCLUSIVE)
score = 95

if score >= 90:
    print("Excellent")  # Runs
elif score >= 80:
    print("Good")       # Skipped
elif score >= 70:
    print("Fair")       # Skipped
# Output: Excellent (ONE thing prints)

# Multiple if (INDEPENDENT)
if score >= 90:
    print("Excellent")  # Runs
if score >= 80:
    print("Good")       # Also runs!
if score >= 70:
    print("Fair")       # Also runs!
# Output:
# Excellent
# Good
# Fair
# (All THREE print! Usually not what you want!)
```

### Common Mistakes

- **Using 'else if' instead of 'elif'**:
  ```python
  # WRONG:
  if score >= 90:
      print("A")
  else if score >= 80:  # SyntaxError! Not valid Python
      print("B")

  # CORRECT:
  if score >= 90:
      print("A")
  elif score >= 80:  # Use 'elif', not 'else if'
      print("B")
  ```

- **Putting else before elif**:
  ```python
  # WRONG:
  if score >= 90:
      print("A")
  else:
      print("Not an A")
  elif score >= 80:  # SyntaxError! elif can't come after else
      print("B")

  # CORRECT:
  if score >= 90:
      print("A")
  elif score >= 80:  # elif before else
      print("B")
  else:              # else always last
      print("Below B")
  ```

- **Redundant conditions in elif**:
  ```python
  # REDUNDANT (but not wrong):
  if score >= 90:
      print("A")
  elif score >= 80 and score < 90:  # 'and score < 90' is redundant!
      print("B")
  # If we're in elif, we KNOW score < 90 (because if was False)

  # CLEANER:
  if score >= 90:
      print("A")
  elif score >= 80:  # Automatically means score < 90
      print("B")
  ```

---
type: "THEORY"
title: "Syntax Breakdown"
---

### elif Chain Anatomy:
```
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

<li>**if**: Always comes first```
if score >= 90:  # First condition to check

```
</li><li>**elif**: Short for "else if" - adds more conditions```
elif score >= 80:  # Only checked if 'if' was False
elif score >= 70:  # Only checked if above were False
# Can have as many elif as needed!

```
</li><li>**else**: Optional catch-all at the end```
else:  # Runs if ALL above were False

```
</li>
### How Python Reads elif Chains:

- **Start at the top**: Check if condition
- **If True**: Execute that block, **skip all remaining** elif/else
- **If False**: Move to next elif, check its condition
- **If no conditions match**: Execute else block (if present)
- **Continue**: Code after the entire chain runs

#### Visual Flow Example:
```
score = 75

if score >= 90:      # False (75 < 90) → Check next
    print("A")
elif score >= 80:    # False (75 < 80) → Check next
    print("B")
elif score >= 70:    # True! (75 >= 70) → Execute & STOP
    print("C")
elif score >= 60:    # SKIPPED (already found match)
    print("D")
else:                 # SKIPPED (already found match)
    print("F")

# Output: C

```
### Order Matters! (Critical Concept)
**Rule:** Put **most specific conditions first**, general ones last.

#### ✅ Correct Order (Specific to General):
```
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
```
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
### elif vs Multiple if Statements:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>elif Chain</th><th>Multiple if</th></tr><tr><td>Mutually exclusive
(only one runs)</td><td>Independent checks
(multiple can run)</td></tr><tr><td>Stops at first match</td><td>Checks every condition</td></tr><tr><td>More efficient</td><td>Less efficient</td></tr><tr><td>Use for categories</td><td>Use for independent flags</td></tr></table>#### Example Comparison:
```
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
### Common Mistakes:

<li>**Using 'else if' instead of 'elif'**:```
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
</li><li>**Putting else before elif**:```
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
</li><li>**Redundant conditions in elif**:```
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
</li>
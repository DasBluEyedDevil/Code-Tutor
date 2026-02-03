---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Missing Colon After Condition**
```python
# WRONG - Missing colon
if age >= 18
    print("Adult")
# SyntaxError: expected ':'

# CORRECT
if age >= 18:
    print("Adult")
```

**2. Using = Instead of == for Comparison**
```python
# WRONG - Assignment, not comparison!
if age = 18:  # SyntaxError!

# CORRECT - Use == to compare
if age == 18:
    print("Exactly 18")
```

**3. Inconsistent Indentation**
```python
# WRONG - Mixing tabs and spaces
if score > 90:
    print("A")    # 4 spaces
        print("Great")  # Tab - IndentationError!

# CORRECT - Always use 4 spaces
if score > 90:
    print("A")
    print("Great")
```

**4. Indenting Code That Should Always Run**
```python
# WRONG - "Done" only prints when condition is True
if temperature > 100:
    print("Boiling")
    print("Done")  # Accidentally indented!

# CORRECT - "Done" always prints
if temperature > 100:
    print("Boiling")
print("Done")  # Not indented = always runs
```

**5. Empty if Block**
```python
# WRONG - Python requires at least one statement
if age >= 18:
    # TODO: add code later
# SyntaxError!

# CORRECT - Use 'pass' as placeholder
if age >= 18:
    pass  # Placeholder for future code
```

---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Creating ExceptionGroup with Empty List**
```python
# WRONG - Empty exception list raises ValueError
errors = []
raise ExceptionGroup("Errors", errors)  # ValueError!

# CORRECT - Only raise if there are errors
if errors:
    raise ExceptionGroup("Errors", errors)
```

**2. Using ExceptionGroup on Python < 3.11**
```python
# WRONG - ExceptionGroup doesn't exist before 3.11
raise ExceptionGroup("Errors", [e1, e2])  # NameError!

# CORRECT - Check version or use exceptiongroup backport
import sys
if sys.version_info < (3, 11):
    from exceptiongroup import ExceptionGroup  # pip install exceptiongroup
```

**3. Catching ExceptionGroup with Regular except**
```python
# WRONG - Regular except catches the group, not individual errors
try:
    raise ExceptionGroup("Errors", [ValueError("a"), TypeError("b")])
except ValueError:  # This won't catch the ValueError inside!
    pass

# CORRECT - Use except* for ExceptionGroups
try:
    raise ExceptionGroup("Errors", [ValueError("a"), TypeError("b")])
except* ValueError as eg:  # Catches ValueErrors inside the group
    print(eg.exceptions)
```

**4. Confusing .exceptions with a List**
```python
# WRONG - .exceptions is a tuple, not a list
eg.exceptions.append(new_error)  # AttributeError!

# CORRECT - .exceptions is immutable tuple
for exc in eg.exceptions:  # Iterate directly
    handle(exc)
```

**5. Raising Single Exception Instead of Group for Validation**
```python
# WRONG - Only first error reported, rest are lost
for field in fields:
    if not valid(field):
        raise ValueError(f"{field} invalid")  # Stops here!

# CORRECT - Collect all errors, raise as group
errors = [ValueError(f"{f} invalid") for f in fields if not valid(f)]
if errors:
    raise ExceptionGroup("Validation failed", errors)
```
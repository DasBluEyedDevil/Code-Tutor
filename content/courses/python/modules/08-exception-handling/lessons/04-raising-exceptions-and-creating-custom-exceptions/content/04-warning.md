---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Raising String Instead of Exception**
```python
# WRONG - Raising a string (Python 2 style, error in Python 3)
raise "Something went wrong"  # TypeError!

# CORRECT - Raise an exception instance
raise ValueError("Something went wrong")
```

**2. Custom Exception Without Inheriting from Exception**
```python
# WRONG - Not inheriting from Exception
class MyError:
    pass

raise MyError()  # Won't be caught by except Exception!

# CORRECT - Inherit from Exception or its subclasses
class MyError(Exception):
    pass

raise MyError("Custom error")  # Works correctly
```

**3. Losing Original Exception When Re-raising**
```python
# WRONG - Original traceback is lost
try:
    risky_operation()
except ValueError:
    raise RuntimeError("Failed")  # Original context lost!

# CORRECT - Chain exceptions to preserve context
try:
    risky_operation()
except ValueError as e:
    raise RuntimeError("Failed") from e  # Preserves original
```

**4. Raising in finally Block**
```python
# WRONG - Exception in finally replaces original
try:
    raise ValueError("Original error")
finally:
    raise RuntimeError("Cleanup error")  # Hides ValueError!

# CORRECT - Handle cleanup errors separately
try:
    raise ValueError("Original error")
finally:
    try:
        cleanup()
    except Exception:
        pass  # Log but don't raise
```

**5. Not Including Helpful Error Messages**
```python
# WRONG - Generic message, no context
def validate_age(age):
    if age < 0:
        raise ValueError("Invalid value")

# CORRECT - Include context in error message
def validate_age(age):
    if age < 0:
        raise ValueError(f"Age must be positive, got {age}")
```
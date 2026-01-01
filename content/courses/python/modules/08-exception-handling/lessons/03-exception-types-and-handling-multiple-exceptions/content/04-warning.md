---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Catching Parent Exception Before Child**
```python
# WRONG - Exception catches everything, specific handlers never run
try:
    value = int("abc")
except Exception:
    print("Generic error")
except ValueError:
    print("Value error")  # Never reached!

# CORRECT - Put specific exceptions first
try:
    value = int("abc")
except ValueError:
    print("Value error")  # This runs
except Exception:
    print("Generic error")  # Fallback
```

**2. Using Exception Type Instead of Instance**
```python
# WRONG - Comparing to class, not instance
try:
    x = 1 / 0
except ZeroDivisionError as e:
    if e == ZeroDivisionError:  # Always False!
        print("Caught it")

# CORRECT - Use isinstance or just handle directly
try:
    x = 1 / 0
except ZeroDivisionError as e:
    print(f"Caught: {e}")  # Use the instance
```

**3. Catching Multiple Exceptions with Wrong Syntax**
```python
# WRONG - This catches Exception, assigns to ValueError variable
try:
    data = process()
except Exception, ValueError:  # Python 2 syntax, error in Python 3
    print("Error")

# CORRECT - Use tuple for multiple exceptions
try:
    data = process()
except (ValueError, TypeError) as e:
    print(f"Error: {e}")
```

**4. Not Re-raising After Logging**
```python
# WRONG - Exception swallowed after logging
try:
    critical_operation()
except Exception as e:
    print(f"Error: {e}")
    # Exception disappears, caller thinks success!

# CORRECT - Re-raise after logging
try:
    critical_operation()
except Exception as e:
    print(f"Error: {e}")
    raise  # Preserve original exception
```

**5. Using str(e) Instead of repr(e) for Debugging**
```python
# WRONG - str() may hide exception type
try:
    data = json.loads("invalid")
except Exception as e:
    print(str(e))  # Shows: "Expecting value: line 1"

# CORRECT - repr() shows exception type
try:
    data = json.loads("invalid")
except Exception as e:
    print(repr(e))  # Shows: JSONDecodeError("Expecting value...")
```
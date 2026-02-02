---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Mixing except* with Regular except**
```python
# WRONG - SyntaxError when mixing except* and except
try:
    risky_operation()
except* ValueError:  # except* block
    pass
except KeyError:     # Regular except - SyntaxError!
    pass

# CORRECT - Use only except* for ExceptionGroups
try:
    risky_operation()
except* ValueError:
    pass
except* KeyError:  # All except* blocks
    pass
```

**2. Expecting a Single Exception Instead of ExceptionGroup**
```python
# WRONG - eg is ExceptionGroup, not single exception
except* ValueError as eg:
    print(str(eg))  # Prints ExceptionGroup, not the ValueError!

# CORRECT - Iterate over eg.exceptions
except* ValueError as eg:
    for exc in eg.exceptions:
        print(str(exc))  # Prints each ValueError
```

**3. Forgetting That Unhandled Exceptions Re-raise**
```python
# WRONG - Assuming all exceptions are handled
try:
    raise ExceptionGroup("E", [ValueError("a"), KeyError("b")])
except* ValueError:
    pass  # KeyError still raises!

# CORRECT - Handle all expected types or catch remaining
try:
    raise ExceptionGroup("E", [ValueError("a"), KeyError("b")])
except* ValueError:
    pass
except* KeyError:
    pass  # Now both are handled
```

**4. Using except* for Non-ExceptionGroup Errors**
```python
# WRONG - except* doesn't catch regular exceptions
try:
    raise ValueError("oops")  # Regular exception
except* ValueError:  # Won't catch it!
    pass

# CORRECT - Use regular except for regular exceptions
try:
    raise ValueError("oops")
except ValueError:  # Regular except
    pass
```

**5. Trying to Suppress with Pass (Remaining Errors Propagate)**
```python
# WRONG - Thinking pass suppresses all
try:
    raise ExceptionGroup("E", [ValueError(), TypeError()])
except* ValueError:
    pass  # TypeError still raises after this!

# CORRECT - To suppress all, catch Exception
try:
    raise ExceptionGroup("E", [ValueError(), TypeError()])
except* Exception:  # Catches all exception types
    pass
```
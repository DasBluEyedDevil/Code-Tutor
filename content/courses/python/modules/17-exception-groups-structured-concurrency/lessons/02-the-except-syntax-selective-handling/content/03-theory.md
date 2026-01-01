---
type: "THEORY"
title: "Syntax Breakdown"
---

**The `except*` rules:**

```python
try:
    risky_operation()
except* ValueError as eg:      # eg is ALWAYS an ExceptionGroup
    for exc in eg.exceptions:   # Even if only one ValueError was caught
        handle(exc)
except* (TypeError, KeyError):  # Catch multiple types
    pass
```

**Key behaviors:**
1. `eg` is always an ExceptionGroup, never a single exception
2. Multiple `except*` blocks can match the same ExceptionGroup
3. Unhandled exceptions are automatically re-raised
4. You can use `raise` to re-raise within `except*`

**What you CAN'T do:**
```python
try:
    ...
except* ValueError:  # except* here...
    ...
except KeyError:     # ...cannot mix with regular except
    ...                # SyntaxError!
```
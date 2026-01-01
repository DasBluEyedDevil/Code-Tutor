---
type: "THEORY"
title: "Syntax Breakdown"
---

**Creating an ExceptionGroup:**
```python
ExceptionGroup(message: str, exceptions: list[Exception])
```

**Accessing sub-exceptions:**
```python
eg.message      # The group's message
eg.exceptions   # Tuple of contained exceptions
len(eg.exceptions)  # Count of exceptions
```

**BaseExceptionGroup vs ExceptionGroup:**
- `ExceptionGroup` - for regular exceptions (ValueError, TypeError, etc.)
- `BaseExceptionGroup` - for base exceptions (KeyboardInterrupt, SystemExit)

**Nesting:** ExceptionGroups can contain other ExceptionGroups - useful for complex hierarchies.
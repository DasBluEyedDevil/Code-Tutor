---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Basic context manager usage:**
```python
with expression as variable:
    # Use variable
    pass
# Cleanup happened automatically
```

**Creating context manager (class-based):**
```python
class MyContext:
    def __enter__(self):
        # Setup
        return resource
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        # Cleanup
        return False  # Don't suppress exceptions

with MyContext() as resource:
    # Use resource
    pass
```

**__exit__ parameters:**
- `exc_type`: Exception class (or None)
- `exc_val`: Exception instance (or None)
- `exc_tb`: Traceback (or None)
- Return True to suppress exception
- Return False/None to propagate exception

**Multiple managers:**
```python
with context1() as c1, context2() as c2:
    # Use both
    pass
```
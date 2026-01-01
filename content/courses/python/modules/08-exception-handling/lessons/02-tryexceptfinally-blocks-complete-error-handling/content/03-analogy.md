---
type: "ANALOGY"
title: "Syntax Breakdown: The Four Parts"
---

**Complete try/except/else/finally structure:**
```python
try:
    # Risky code that might fail
    risky_operation()
    
except SpecificError1:
    # Handle this specific error
    handle_error_1()
    
except SpecificError2:
    # Handle a different error
    handle_error_2()
    
else:
    # Runs ONLY if NO exception occurred
    success_operations()
    
finally:
    # Runs NO MATTER WHAT
    cleanup_code()
```

**When each part runs:**

**try block:**
- ALWAYS runs first
- Stops at the first exception

**except blocks:**
- Run ONLY if an exception occurs in try
- Python checks each except block in order
- Only the FIRST matching except runs
- Can have multiple except blocks for different errors

**else block (optional):**
- Runs ONLY if try completed WITHOUT any exception
- Doesn't run if an exception occurred
- Good for "success-only" code
- Must come after except blocks, before finally

**finally block (optional):**
- Runs NO MATTER WHAT happens
- Runs after try succeeds
- Runs after except handles error
- Runs even if there's a return statement
- Runs even if a new exception occurs
- Perfect for cleanup: closing files, releasing resources, logging

**Multiple except blocks syntax:**
```python
except ValueError:
    # Handle ValueError
    
except ZeroDivisionError:
    # Handle ZeroDivisionError
    
except (TypeError, KeyError):  # Multiple in one block
    # Handle either TypeError or KeyError
```

**Capturing the exception object:**
```python
except ValueError as e:
    print(f"Error details: {e}")
    # 'e' contains the exception object with error info
```
---
type: "EXAMPLE"
title: "Code Example: Custom Context Managers"
---

**Two ways to create context managers:**

**1. Class-based (more control):**
```python
class MyContext:
    def __enter__(self): ...
    def __exit__(self, ...): ...
```
- More verbose
- Full control over behavior
- Can maintain complex state

**2. Function-based with @contextmanager:**
```python
@contextmanager
def my_context():
    # Setup
    yield resource
    # Cleanup
```
- Simpler syntax
- Code before yield = __enter__
- Code after yield = __exit__
- Must use try/finally for proper cleanup

**Suppressing exceptions:**
- Return True from __exit__ to suppress
- Use carefully - can hide bugs!

```python
import time
from contextlib import contextmanager

print("=== Class-Based Context Manager ===")

class Timer:
    """Context manager for timing code blocks"""
    
    def __init__(self, name="Code block"):
        self.name = name
        self.start_time = None
        self.elapsed = None
    
    def __enter__(self):
        print(f"Starting timer: {self.name}")
        self.start_time = time.time()
        return self
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        self.elapsed = time.time() - self.start_time
        print(f"Finished: {self.name} took {self.elapsed:.4f}s")
        return False

with Timer("Sleep test"):
    time.sleep(0.1)

print()

class DatabaseConnection:
    """Simulates database connection manager"""
    
    def __init__(self, db_name):
        self.db_name = db_name
        self.connected = False
    
    def __enter__(self):
        print(f"  Connecting to {self.db_name}...")
        self.connected = True
        print(f"  Connected!")
        return self
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        if exc_type:
            print(f"  Rolling back due to error: {exc_val}")
        else:
            print(f"  Committing changes...")
        print(f"  Closing connection to {self.db_name}")
        self.connected = False
        return False
    
    def execute(self, query):
        if not self.connected:
            raise RuntimeError("Not connected!")
        print(f"  Executing: {query}")
        return "Success"

print("\nSuccessful transaction:")
with DatabaseConnection("users.db") as db:
    db.execute("SELECT * FROM users")
    db.execute("UPDATE users SET active=1")

print("\nTransaction with error:")
try:
    with DatabaseConnection("users.db") as db:
        db.execute("SELECT * FROM users")
        raise ValueError("Something went wrong!")
        db.execute("This never runs")
except ValueError:
    print("  Error handled\n")

print("=== Function-Based with @contextmanager ===")

@contextmanager
def timer(name):
    """Function-based timer context manager"""
    print(f"Starting: {name}")
    start = time.time()
    try:
        yield  # Code block runs here
    finally:
        elapsed = time.time() - start
        print(f"Finished: {name} took {elapsed:.4f}s")

with timer("Quick operation"):
    time.sleep(0.05)
    print("  Doing work...")

print()

@contextmanager
def temporary_directory_change(path):
    """Temporarily change directory"""
    import os
    original = os.getcwd()
    print(f"  Changing to: {path}")
    os.chdir(path)
    try:
        yield original
    finally:
        print(f"  Restoring to: {original}")
        os.chdir(original)

print("Current directory example:")
import os
print(f"Before: {os.getcwd()}")
with temporary_directory_change('/tmp'):
    print(f"Inside: {os.getcwd()}")
print(f"After: {os.getcwd()}")

print("\n=== Suppressing Exceptions ===")

class IgnoreErrors:
    """Context manager that suppresses exceptions"""
    
    def __init__(self, *exception_types):
        self.exception_types = exception_types or (Exception,)
    
    def __enter__(self):
        return self
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        if exc_type and issubclass(exc_type, self.exception_types):
            print(f"  Suppressed: {exc_type.__name__}: {exc_val}")
            return True  # Suppress the exception
        return False

print("Suppressing ValueError:")
with IgnoreErrors(ValueError):
    print("  Before error")
    raise ValueError("This will be suppressed")
    print("  This won't run")
print("  Continued after context\n")

print("Not suppressing TypeError:")
try:
    with IgnoreErrors(ValueError):
        print("  Before error")
        raise TypeError("This will NOT be suppressed")
except TypeError as e:
    print(f"  Caught: {e}")
```

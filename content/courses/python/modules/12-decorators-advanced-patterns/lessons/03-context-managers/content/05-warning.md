---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting to Return the Resource in __enter__**
```python
# WRONG - __enter__ returns None
class FileManager:
    def __init__(self, filename):
        self.file = open(filename, 'r')
    
    def __enter__(self):
        pass  # Returns None!
    
    def __exit__(self, *args):
        self.file.close()

with FileManager('data.txt') as f:
    f.read()  # AttributeError: 'NoneType' has no attribute 'read'

# CORRECT - Return the resource
class FileManager:
    def __enter__(self):
        return self.file  # Return the resource!
```

**2. Missing try/finally in @contextmanager**
```python
# WRONG - Cleanup doesn't run on exception
from contextlib import contextmanager

@contextmanager
def managed_resource():
    print('Setup')
    yield 'resource'
    print('Cleanup')  # NEVER runs if exception!

# CORRECT - Use try/finally
@contextmanager
def managed_resource():
    print('Setup')
    try:
        yield 'resource'
    finally:
        print('Cleanup')  # Always runs!
```

**3. Returning True in __exit__ Accidentally**
```python
# WRONG - Silently swallows exceptions
class BadManager:
    def __exit__(self, exc_type, exc_val, exc_tb):
        print('Cleaning up')
        return True  # Suppresses ALL exceptions!

with BadManager():
    raise ValueError('Error!')  # Silently ignored!

# CORRECT - Return False or None
class GoodManager:
    def __exit__(self, exc_type, exc_val, exc_tb):
        print('Cleaning up')
        return False  # Let exceptions propagate
```

**4. Using Resource After Context Closes**
```python
# WRONG - Using resource after context ends
with open('data.txt', 'r') as f:
    pass  # Context ends here

content = f.read()  # ValueError: I/O operation on closed file

# CORRECT - Read inside the context
with open('data.txt', 'r') as f:
    content = f.read()  # Read while file is open

print(content)  # Use the data after
```

**5. Multiple yields in @contextmanager**
```python
# WRONG - Multiple yields cause RuntimeError
from contextlib import contextmanager

@contextmanager
def bad_manager():
    yield 'first'
    yield 'second'  # RuntimeError: generator didn't stop!

# CORRECT - Exactly one yield
@contextmanager
def good_manager():
    setup()
    try:
        yield 'resource'  # Only ONE yield
    finally:
        cleanup()
```
---
type: "EXAMPLE"
title: "Code Example: Understanding with Statement"
---

**The `with` statement:**

1. **Calls `__enter__()`:**
   - Runs setup code
   - Returns resource (file object)

2. **Executes code block:**
   - Your code runs
   - Uses the resource

3. **Calls `__exit__()`:**
   - Runs cleanup code
   - ALWAYS executes, even on errors
   - Receives exception info if error occurred

**Multiple context managers:**
```python
# Old way
with open('a.txt') as f1:
    with open('b.txt') as f2:
        ...

# Modern way (Python 3.1+)
with open('a.txt') as f1, open('b.txt') as f2:
    ...
```

```python
print("=== Without Context Manager ===")

# Manual resource management (error-prone)
def read_file_manual(filename):
    file = None
    try:
        file = open(filename, 'r')
        content = file.read()
        return content
    except Exception as e:
        print(f"Error: {e}")
        return None
    finally:
        if file:
            file.close()
            print("File closed manually")

# Create test file
with open('test.txt', 'w') as f:
    f.write('Hello, World!')

result = read_file_manual('test.txt')
print(f"Content: {result}\n")

print("=== With Context Manager ===")

# Clean and simple
def read_file_context(filename):
    with open(filename, 'r') as file:
        content = file.read()
        return content
    # File automatically closed here!

result = read_file_context('test.txt')
print(f"Content: {result}")
print("File automatically closed\n")

print("=== Context Manager with Error ===")

try:
    with open('test.txt', 'r') as file:
        content = file.read()
        print(f"Read: {content}")
        raise ValueError("Simulated error!")
        print("This never executes")
except ValueError as e:
    print(f"Caught: {e}")
    print("File was still closed automatically!\n")

print("=== Multiple Context Managers ===")

# Create two files
with open('input.txt', 'w') as f:
    f.write('Line 1\nLine 2\nLine 3')

# Use multiple context managers
with open('input.txt', 'r') as infile, open('output.txt', 'w') as outfile:
    for line in infile:
        outfile.write(line.upper())
    print("Copied and uppercased to output.txt")
# Both files automatically closed

with open('output.txt', 'r') as f:
    print(f"Output: {f.read()}")

print("\n=== What Happens Behind the Scenes ===")

class FileSimulator:
    """Simulates what happens with context manager"""
    def __init__(self, filename):
        self.filename = filename
        self.file = None
    
    def __enter__(self):
        print(f"  __enter__ called: Opening {self.filename}")
        self.file = open(self.filename, 'r')
        return self.file
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        print(f"  __exit__ called: Closing {self.filename}")
        if self.file:
            self.file.close()
        if exc_type:
            print(f"  Exception occurred: {exc_type.__name__}: {exc_val}")
        return False  # Don't suppress exceptions

print("Using custom FileSimulator:")
with FileSimulator('test.txt') as f:
    content = f.read()
    print(f"  Read content: {content}")
print("  Back outside context\n")

import os
for filename in ['test.txt', 'input.txt', 'output.txt']:
    if os.path.exists(filename):
        os.remove(filename)
```

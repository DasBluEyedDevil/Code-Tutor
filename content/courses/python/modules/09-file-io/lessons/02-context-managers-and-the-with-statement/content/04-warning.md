---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using File After with Block Ends**
```python
# WRONG - File is closed after with block
with open("data.txt") as f:
    pass
content = f.read()  # ValueError: I/O operation on closed file!

# CORRECT - Read inside the with block
with open("data.txt") as f:
    content = f.read()
# Use content outside, not f
```

**2. Nested with Statements When One Would Do**
```python
# WRONG - Overly nested, harder to read
with open("input.txt") as f1:
    with open("output.txt", "w") as f2:
        data = f1.read()
        f2.write(data)

# CORRECT - Multiple files in one with statement
with open("input.txt") as f1, open("output.txt", "w") as f2:
    data = f1.read()
    f2.write(data)
```

**3. Returning Inside with Block Without Reading**
```python
# WRONG - File handle returned but closed when function ends
def get_file():
    with open("data.txt") as f:
        return f  # Returns closed file!

# CORRECT - Return content, not file handle
def get_content():
    with open("data.txt") as f:
        return f.read()  # Return content
```

**4. Not Specifying Encoding**
```python
# WRONG - Uses system default, may fail on special characters
with open("data.txt") as f:
    content = f.read()  # May fail on non-ASCII!

# CORRECT - Specify encoding explicitly
with open("data.txt", encoding="utf-8") as f:
    content = f.read()
```

**5. Assuming with Handles All Exceptions**
```python
# WRONG - with only handles file closing, not all errors
with open("data.txt") as f:
    data = json.load(f)  # JSONDecodeError crashes program!

# CORRECT - Still need try/except for processing errors
try:
    with open("data.txt") as f:
        data = json.load(f)
except json.JSONDecodeError:
    data = {}
except FileNotFoundError:
    data = {}
```
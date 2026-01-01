---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting to Close Files**
```python
# WRONG - File never closed, resource leak
file = open("data.txt", "r")
content = file.read()
# Program ends without file.close()

# CORRECT - Always close files
file = open("data.txt", "r")
content = file.read()
file.close()

# BEST - Use with statement (auto-closes)
with open("data.txt", "r") as file:
    content = file.read()
# File automatically closed here
```

**2. Using Wrong Mode for Operation**
```python
# WRONG - Opened for reading, trying to write
file = open("data.txt", "r")
file.write("Hello")  # io.UnsupportedOperation!

# CORRECT - Use correct mode
file = open("data.txt", "w")  # 'w' for write
file.write("Hello")
file.close()
```

**3. Overwriting File When Meaning to Append**
```python
# WRONG - 'w' mode erases existing content!
with open("log.txt", "w") as f:
    f.write("New entry")  # Old logs gone!

# CORRECT - Use 'a' mode to append
with open("log.txt", "a") as f:
    f.write("New entry")  # Old logs preserved
```

**4. Not Handling File Not Found**
```python
# WRONG - Crashes if file doesn't exist
with open("config.txt", "r") as f:
    config = f.read()  # FileNotFoundError!

# CORRECT - Handle missing file gracefully
try:
    with open("config.txt", "r") as f:
        config = f.read()
except FileNotFoundError:
    config = "default settings"
```

**5. Forgetting Newlines When Writing**
```python
# WRONG - All text runs together
with open("lines.txt", "w") as f:
    f.write("Line 1")
    f.write("Line 2")  # File contains: "Line 1Line 2"

# CORRECT - Add newlines explicitly
with open("lines.txt", "w") as f:
    f.write("Line 1\n")
    f.write("Line 2\n")  # File contains proper lines
```
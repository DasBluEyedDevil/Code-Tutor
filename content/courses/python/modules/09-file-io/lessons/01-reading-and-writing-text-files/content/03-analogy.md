---
type: "ANALOGY"
title: "Syntax Breakdown: File Operations"
---

**Opening Files:**
```python
# Basic syntax
file_object = open(filename, mode)

# Examples
file = open("data.txt", "r")   # Read mode
file = open("output.txt", "w") # Write mode
file = open("log.txt", "a")    # Append mode
```

**Reading Methods:**

**read() - Read entire file as one string:**
```python
file = open("file.txt", "r")
content = file.read()  # "Line 1\nLine 2\nLine 3\n"
file.close()
```

**readline() - Read one line at a time:**
```python
file = open("file.txt", "r")
line1 = file.readline()  # "Line 1\n"
line2 = file.readline()  # "Line 2\n"
file.close()
```

**readlines() - Read all lines into a list:**
```python
file = open("file.txt", "r")
lines = file.readlines()  # ["Line 1\n", "Line 2\n", "Line 3\n"]
file.close()
```

**Iterating over file lines (memory efficient):**
```python
file = open("file.txt", "r")
for line in file:  # Reads one line at a time
    print(line.strip())
file.close()
```

**Writing Methods:**

**write() - Write a string:**
```python
file = open("file.txt", "w")
file.write("Hello\n")  # Must add \n yourself!
file.write("World\n")
file.close()
```

**writelines() - Write a list of strings:**
```python
file = open("file.txt", "w")
lines = ["Line 1\n", "Line 2\n", "Line 3\n"]
file.writelines(lines)  # Doesn't add \n automatically!
file.close()
```

**File Modes Quick Reference:**

| Mode | Description | Creates File? | Overwrites? |
|------|-------------|---------------|-------------|
| 'r'  | Read only   | No (error)    | N/A         |
| 'w'  | Write       | Yes           | YES         |
| 'a'  | Append      | Yes           | No          |
| 'r+' | Read+Write  | No (error)    | No          |
| 'w+' | Write+Read  | Yes           | YES         |
| 'a+' | Append+Read | Yes           | No          |

**Important notes:**

1. **Always close files:** file.close() or use `with` (next lesson)

2. **Write mode ('w') overwrites:** All existing content is DELETED!

3. **Read mode requires file exists:** FileNotFoundError if not found

4. **Line endings:** write() doesn't add \n automatically - you must add it

5. **strip() removes \n:** When reading lines, use .strip() to remove newline characters

**Common pattern for reading:**
```python
try:
    file = open("data.txt", "r")
    content = file.read()
    file.close()
except FileNotFoundError:
    print("File not found!")
```

**Common pattern for writing:**
```python
file = open("output.txt", "w")
file.write("Line 1\n")
file.write("Line 2\n")
file.close()  # IMPORTANT: Saves changes!
```
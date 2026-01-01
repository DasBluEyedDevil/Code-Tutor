---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Reading Entire Large Log File Into Memory**
```python
# WRONG - Loads entire file (could be gigabytes!)
with open("huge.log") as f:
    content = f.read()
    for line in content.split("\n"):
        process(line)

# CORRECT - Read line by line
with open("huge.log") as f:
    for line in f:  # Iterates without loading all
        process(line)
```

**2. Not Handling Log Rotation**
```python
# WRONG - Misses logs when file is rotated
with open("app.log") as f:
    while True:
        line = f.readline()  # Stops at EOF, misses new file

# CORRECT - Reopen file periodically or use watchdog
import time
while True:
    with open("app.log") as f:
        for line in f:
            process(line)
    time.sleep(1)  # Check for new content
```

**3. Using print for Log Output**
```python
# WRONG - No timestamps, no levels, hard to filter
print("Error: something failed")
print("User logged in")

# CORRECT - Use logging module
import logging
logging.basicConfig(level=logging.INFO)
logging.error("Something failed")
logging.info("User logged in")
```

**4. Regex for Every Line of Large Files**
```python
# WRONG - Compiling regex inside loop is slow
import re
for line in open("huge.log"):
    if re.match(r"ERROR.*", line):  # Recompiles every time!
        print(line)

# CORRECT - Compile regex once outside loop
import re
pattern = re.compile(r"ERROR.*")
for line in open("huge.log"):
    if pattern.match(line):
        print(line)
```

**5. Not Handling Encoding Errors in Logs**
```python
# WRONG - Crashes on binary/corrupt data
with open("mixed.log") as f:
    for line in f:  # UnicodeDecodeError on binary!
        print(line)

# CORRECT - Handle encoding errors
with open("mixed.log", encoding="utf-8", errors="replace") as f:
    for line in f:
        print(line)
```
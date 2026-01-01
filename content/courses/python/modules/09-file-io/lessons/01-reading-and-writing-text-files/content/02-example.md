---
type: "EXAMPLE"
title: "Code Example: Reading and Writing Files"
---

Key concepts:
1. **open(filename, mode)** opens a file and returns a file object
2. **read()** reads entire file as one string
3. **readline()** reads one line at a time
4. **readlines()** reads all lines into a list
5. **write(string)** writes content (must include \n for new lines)
6. **close()** MUST be called to save changes and free resources
7. **File modes:** 'r' (read), 'w' (write/overwrite), 'a' (append)

Without close(), changes may not be saved!

```python
# Example 1: Writing to a file
print("=== Writing to a File ===")

# Open file in WRITE mode (creates if doesn't exist, overwrites if exists)
file = open("greeting.txt", "w")

# Write content (note: must add \n for new lines yourself)
file.write("Hello, World!\n")
file.write("Welcome to Python file handling.\n")
file.write("This is line 3.\n")

# MUST close the file to save changes!
file.close()

print("✓ Created greeting.txt\n")

# Example 2: Reading an entire file
print("=== Reading Entire File ===")

# Open file in READ mode
file = open("greeting.txt", "r")

# Read all content at once
content = file.read()
print("Content:")
print(content)

# Close the file
file.close()
print("✓ File closed\n")

# Example 3: Reading line by line
print("=== Reading Line by Line ===")

file = open("greeting.txt", "r")

# readline() reads ONE line at a time
line1 = file.readline()
line2 = file.readline()
line3 = file.readline()

print(f"Line 1: {line1.strip()}")  # .strip() removes \n
print(f"Line 2: {line2.strip()}")
print(f"Line 3: {line3.strip()}")

file.close()
print("")

# Example 4: Reading all lines into a list
print("=== Reading All Lines as List ===")

file = open("greeting.txt", "r")

# readlines() returns a list of all lines
lines = file.readlines()
print(f"Number of lines: {len(lines)}")

for i, line in enumerate(lines, 1):
    print(f"  {i}. {line.strip()}")

file.close()
print("")

# Example 5: Appending to a file
print("=== Appending to a File ===")

# APPEND mode - adds to end without erasing existing content
file = open("greeting.txt", "a")

file.write("This line was appended.\n")
file.write("So was this one!\n")

file.close()

print("✓ Appended content\n")

# Read updated file
print("=== Updated File Content ===")
file = open("greeting.txt", "r")
print(file.read())
file.close()

# Example 6: Common file modes
print("=== File Modes Reference ===")
print("'r'  - Read (default). File must exist.")
print("'w'  - Write. Creates file or OVERWRITES existing.")
print("'a'  - Append. Creates file or adds to end.")
print("'r+' - Read and Write. File must exist.")
print("'w+' - Write and Read. Creates or overwrites.")
print("'a+' - Append and Read. Creates or adds to end.")
print("")

# Example 7: Error handling with files
print("=== Error Handling ===")

try:
    file = open("nonexistent.txt", "r")
    content = file.read()
    file.close()
except FileNotFoundError:
    print("❌ Error: File 'nonexistent.txt' does not exist!")
    print("   Use 'w' or 'a' mode to create it, or check the filename.")

print("\n✓ Program continues despite error")
```

---
type: "EXAMPLE"
title: "Code Example: with Statement in Action"
---

The with statement provides:
1. **Automatic resource management** - file closes when block ends
2. **Exception safety** - file closes even if error occurs
3. **Cleaner syntax** - no need for try/finally
4. **Multiple resources** - can open multiple files in one with
5. **Iteration support** - can iterate over file object directly

The syntax: with open(filename, mode) as variable: means "open this file, call it 'variable', and auto-close when done."

```python
# Example 1: Basic with statement
print("=== Basic with Statement ===")

# Write to file using with
with open("demo.txt", "w") as file:
    file.write("Line 1\n")
    file.write("Line 2\n")
    file.write("Line 3\n")
# File automatically closed here!

print("✓ File written and auto-closed")

# Read from file using with
with open("demo.txt", "r") as file:
    content = file.read()
    print("\nContent:")
    print(content)
# File automatically closed here!

print("✓ File read and auto-closed\n")

# Example 2: with handles errors automatically
print("=== Error Handling with 'with' ===")

try:
    with open("demo.txt", "r") as file:
        print("Reading file...")
        content = file.read()
        print("File content retrieved")
        
        # Simulate an error
        raise ValueError("Simulated error!")
        
        print("This line never runs")
        
except ValueError as e:
    print(f"Error occurred: {e}")
    print("But file was STILL closed automatically!\n")

# Example 3: Multiple files at once
print("=== Opening Multiple Files ===")

# Write source file
with open("source.txt", "w") as file:
    file.write("This is the source content.\n")

print("✓ Source file created")

# Copy from one file to another
with open("source.txt", "r") as source, \
     open("destination.txt", "w") as dest:
    
    # Read from source
    content = source.read()
    
    # Write to destination
    dest.write(content)
    dest.write("This line was added during copy.\n")

print("✓ File copied")

# Verify
with open("destination.txt", "r") as file:
    print("\nDestination content:")
    print(file.read())

# Example 4: with vs. manual closing comparison
print("=== Comparison: with vs. Manual ===")

print("\nManual way (old, error-prone):")
try:
    file = open("manual.txt", "w")
    file.write("Manual closing\n")
finally:
    file.close()
    print("  ✓ Had to remember to close in finally")

print("\nWith statement (modern, safe):")
with open("with.txt", "w") as file:
    file.write("Automatic closing\n")
print("  ✓ Automatically closed, no finally needed")

# Example 5: Reading file line by line with 'with'
print("\n=== Line-by-Line Reading ===")

# Create test file
with open("lines.txt", "w") as file:
    for i in range(1, 6):
        file.write(f"Line {i}: Some content here\n")

print("✓ Created test file")

# Read line by line
print("\nReading line by line:")
with open("lines.txt", "r") as file:
    for line_num, line in enumerate(file, 1):
        print(f"  {line_num}. {line.strip()}")

print("\n✓ File automatically closed after iteration")

# Example 6: Appending with 'with'
print("\n=== Appending to File ===")

with open("log.txt", "w") as file:
    file.write("Log started\n")

print("✓ Log file created")

with open("log.txt", "a") as file:
    file.write("Entry 1: User logged in\n")
    file.write("Entry 2: User viewed dashboard\n")
    file.write("Entry 3: User logged out\n")

print("✓ Entries appended")

with open("log.txt", "r") as file:
    print("\nLog content:")
    print(file.read())

print("=== All examples completed ===")
```

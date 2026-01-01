---
type: "EXAMPLE"
title: "Code Example: The Complete Structure"
---

The complete structure:
1. **try**: Attempt the risky operation
2. **except**: Handle specific errors (can have multiple except blocks)
3. **else**: Runs ONLY if no exception occurred (optional)
4. **finally**: Runs NO MATTER WHAT - even if there's a return statement (optional but powerful)

Notice how finally runs even when we return early in the except blocks!

```python
# Example 1: File handling with finally
print("=== File Handling Example ===")

file_opened = False
try:
    print("Attempting to read file...")
    # Simulating file operations
    filename = "data.txt"
    print(f"Opening {filename}")
    file_opened = True
    
    # Simulate processing - this might fail!
    # Uncomment next line to simulate an error:
    # raise ValueError("Data format error!")
    
    print("Processing file data...")
    print("File processed successfully!")
    
except FileNotFoundError:
    print("ERROR: File not found!")
    print("Please check the filename and try again.")
    
except ValueError as e:
    print(f"ERROR: Invalid data in file: {e}")
    print("File may be corrupted.")
    
else:
    # This runs ONLY if NO exception occurred
    print("SUCCESS: All operations completed without errors!")
    print("Ready to use the data.")
    
finally:
    # This runs NO MATTER WHAT - success, error, or return
    print("\n--- CLEANUP (Finally block) ---")
    if file_opened:
        print("Closing file...")
        print("File closed successfully.")
    print("Cleanup complete!")
    print("--- End of operation ---\n")

# Example 2: Division with complete error handling
print("=== Division Calculator ===")

def safe_divide(a, b):
    result = None
    try:
        print(f"Attempting to divide {a} by {b}")
        result = a / b
        
    except ZeroDivisionError:
        print("ERROR: Cannot divide by zero!")
        return None
        
    except TypeError:
        print("ERROR: Both values must be numbers!")
        return None
        
    else:
        # Runs only if division succeeded
        print(f"Division successful: {a} / {b} = {result}")
        
    finally:
        # Runs no matter what - even if we returned early!
        print("[Finally: Logging this operation to system]")
        
    return result

# Test cases
print("Test 1: Normal division")
result1 = safe_divide(10, 2)
print(f"Result: {result1}\n")

print("Test 2: Division by zero")
result2 = safe_divide(10, 0)
print(f"Result: {result2}\n")

print("Test 3: Invalid type")
result3 = safe_divide(10, "two")
print(f"Result: {result3}")
```

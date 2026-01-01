---
type: "EXAMPLE"
title: "Code Example: The Crash vs. The Catch"
---

The try block contains code that might fail. If an error occurs, instead of crashing, Python jumps to the matching except block and runs that code instead. The program continues after the except block!

```python
# WITHOUT ERROR HANDLING - Program crashes!
print("=== Without Error Handling ===")
print("Enter your age:")
# If user types 'twenty' instead of 20, this crashes:
# age = int(input())  # Commented to prevent crash in example
# print(f"You are {age} years old")
print("Program would crash here if user typed 'twenty'\n")

# WITH ERROR HANDLING - Program handles it gracefully!
print("=== With Error Handling ===")
print("Enter your age:")

try:
    # This is the "risky" code that might fail
    age_input = "twenty"  # Simulating user typing 'twenty'
    age = int(age_input)  # This will cause an error!
    print(f"You are {age} years old")
except ValueError:
    # This code runs if the error happens
    print("Oops! That's not a valid number.")
    print("Please enter your age as a number (e.g., 25)")
    age = 25  # Set a default or ask again

print("Program continues running!")
print(f"Age set to: {age}")

# Another example: Division by zero
print("\n=== Division Example ===")

try:
    result = 10 / 0  # This will cause a ZeroDivisionError!
    print(f"Result: {result}")
except ZeroDivisionError:
    print("Cannot divide by zero!")
    print("Setting result to 0")
    result = 0

print(f"Final result: {result}")
print("Program finished successfully!")
```

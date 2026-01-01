---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using Bare except: Without Specifying Exception Type**
```python
# WRONG - Catches everything, hides bugs
try:
    result = int(user_input)
except:
    print("Error occurred")

# CORRECT - Catch specific exceptions
try:
    result = int(user_input)
except ValueError:
    print("Please enter a valid number")
```

**2. Catching Exception Too Broadly**
```python
# WRONG - Catches too much, including KeyboardInterrupt
try:
    data = process_data()
except Exception:
    pass  # Silently ignores ALL errors

# CORRECT - Catch only expected exceptions
try:
    data = process_data()
except (ValueError, TypeError) as e:
    print(f"Data error: {e}")
```

**3. Empty except Block (Swallowing Errors)**
```python
# WRONG - Error is silently ignored
try:
    file = open("data.txt")
except FileNotFoundError:
    pass  # Nothing happens, bug hidden

# CORRECT - At least log the error
try:
    file = open("data.txt")
except FileNotFoundError:
    print("Warning: data.txt not found, using defaults")
    file = None
```

**4. Putting Too Much Code in try Block**
```python
# WRONG - Too much code in try block
try:
    user_input = input("Enter number: ")
    number = int(user_input)
    result = number * 2
    print(f"Result: {result}")
    save_to_file(result)
except ValueError:
    print("Invalid input")

# CORRECT - Only risky code in try block
user_input = input("Enter number: ")
try:
    number = int(user_input)
except ValueError:
    print("Invalid input")
    number = 0
result = number * 2
print(f"Result: {result}")
```

**5. Confusing Syntax Errors with Exceptions**
```python
# WRONG - Syntax errors can't be caught at runtime
try:
    if True  # Missing colon - SyntaxError
        print("Hello")
except SyntaxError:
    print("Caught it!")  # Never runs!

# CORRECT - Only runtime exceptions can be caught
try:
    int("not a number")  # ValueError at runtime
except ValueError:
    print("Caught it!")  # This works!
```
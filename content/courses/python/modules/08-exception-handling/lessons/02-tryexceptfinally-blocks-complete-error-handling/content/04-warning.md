---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Wrong Order of try/except/else/finally**
```python
# WRONG - else must come before finally
try:
    result = int("42")
finally:
    print("Cleanup")
else:
    print("Success")  # SyntaxError!

# CORRECT - Order: try, except, else, finally
try:
    result = int("42")
except ValueError:
    print("Error")
else:
    print("Success")
finally:
    print("Cleanup")
```

**2. Returning in finally Overwrites try/except Returns**
```python
# WRONG - finally return overwrites everything
def get_value():
    try:
        return 42
    finally:
        return 0  # This always returns!

print(get_value())  # Prints 0, not 42!

# CORRECT - Don't return in finally
def get_value():
    try:
        return 42
    finally:
        print("Cleanup done")  # Just cleanup, no return
```

**3. Forgetting else Does Not Run After Exception**
```python
# WRONG - Expecting else to always run
try:
    data = int("not a number")
except ValueError:
    data = 0
else:
    print("Processing data")  # Never runs after exception!

# CORRECT - Put shared code after the block
try:
    data = int("not a number")
except ValueError:
    data = 0
# Code here runs regardless
process(data)
```

**4. Modifying Mutable Objects in finally**
```python
# WRONG - Modifying list in finally affects return
def get_list():
    result = [1, 2, 3]
    try:
        return result
    finally:
        result.clear()  # Clears the list before return!

print(get_list())  # Prints [] not [1, 2, 3]!

# CORRECT - Return a copy if modifying in finally
def get_list():
    result = [1, 2, 3]
    try:
        return result.copy()
    finally:
        result.clear()
```

**5. Using else When You Should Use finally**
```python
# WRONG - Cleanup in else doesn't run on error
file = open("data.txt")
try:
    data = file.read()
except IOError:
    print("Read error")
else:
    file.close()  # Won't close on error!

# CORRECT - Use finally for guaranteed cleanup
file = open("data.txt")
try:
    data = file.read()
except IOError:
    print("Read error")
finally:
    file.close()  # Always closes
```
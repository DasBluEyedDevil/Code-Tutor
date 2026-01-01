---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Adding a Condition to else**
```python
# WRONG - else doesn't take a condition!
if age >= 18:
    print("Adult")
else age < 18:  # SyntaxError!
    print("Minor")

# CORRECT - else has no condition
if age >= 18:
    print("Adult")
else:
    print("Minor")
```

**2. else Not Aligned with if**
```python
# WRONG - else must be at same indentation as if
if age >= 18:
    print("Adult")
    else:  # IndentationError!
        print("Minor")

# CORRECT - else aligns with if
if age >= 18:
    print("Adult")
else:
    print("Minor")
```

**3. Missing Colon After else**
```python
# WRONG - else needs a colon
if age >= 18:
    print("Adult")
else  # SyntaxError: expected ':'
    print("Minor")

# CORRECT
if age >= 18:
    print("Adult")
else:
    print("Minor")
```

**4. Using if-else When You Need elif**
```python
# WRONG - Only handles 2 cases, but there are more
if grade >= 90:
    print("A")
else:
    print("Not an A")  # What about B, C, D, F?

# CORRECT - Use elif for multiple cases
if grade >= 90:
    print("A")
elif grade >= 80:
    print("B")
elif grade >= 70:
    print("C")
else:
    print("Below C")
```

**5. Variable Scope Confusion**
```python
# WRONG - Variable might not be defined
if condition:
    message = "Success"
# If condition is False, message doesn't exist!
print(message)  # NameError if condition was False

# CORRECT - Define in both branches
if condition:
    message = "Success"
else:
    message = "Failed"
print(message)  # Always works
```
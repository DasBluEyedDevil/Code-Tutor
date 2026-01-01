---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting to Call the Function (Just Referencing It)**
```python
# WRONG - This just references the function, doesn't run it!
def greet():
    print("Hello!")

greet  # Nothing happens! Missing parentheses

# CORRECT - Call the function with parentheses
greet()  # Prints: Hello!
```

**2. Not Indenting Code Inside the Function**
```python
# WRONG - Code not indented
def greet():
print("Hello!")  # IndentationError!

# CORRECT - Code is properly indented
def greet():
    print("Hello!")  # This is part of the function
```

**3. Calling a Function Before Defining It**
```python
# WRONG - Calling before defining
greet()  # NameError: name 'greet' is not defined

def greet():
    print("Hello!")

# CORRECT - Define first, then call
def greet():
    print("Hello!")

greet()  # Works!
```

**4. Using Inconsistent Naming Conventions**
```python
# WRONG - Inconsistent naming styles
def CalculateTotal():  # PascalCase (for classes, not functions)
    pass
def calculatetotal():  # No separation between words
    pass

# CORRECT - Use snake_case for functions
def calculate_total():
    pass
def send_email_notification():
    pass
```

**5. Forgetting the Colon After the Function Definition**
```python
# WRONG - Missing colon
def greet()  # SyntaxError!
    print("Hello!")

# CORRECT - Include the colon
def greet():  # Colon is required!
    print("Hello!")
```
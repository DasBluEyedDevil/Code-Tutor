---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting @wraps - Losing Function Metadata**
```python
# WRONG - Original function name/docstring is lost
def my_decorator(func):
    def wrapper(*args, **kwargs):
        return func(*args, **kwargs)
    return wrapper

@my_decorator
def greet():
    """Says hello"""
    pass

print(greet.__name__)  # Prints 'wrapper', not 'greet'

# CORRECT - Preserve metadata with @wraps
from functools import wraps

def my_decorator(func):
    @wraps(func)
    def wrapper(*args, **kwargs):
        return func(*args, **kwargs)
    return wrapper
```

**2. Not Returning the Wrapper Function**
```python
# WRONG - Decorator doesn't return wrapper
def my_decorator(func):
    def wrapper(*args, **kwargs):
        return func(*args, **kwargs)
    # Missing return!

@my_decorator
def greet():
    print("Hello")

greet()  # TypeError: 'NoneType' object is not callable

# CORRECT - Always return the wrapper
def my_decorator(func):
    def wrapper(*args, **kwargs):
        return func(*args, **kwargs)
    return wrapper  # Don't forget this!
```

**3. Forgetting *args and **kwargs in Wrapper**
```python
# WRONG - Wrapper doesn't accept arguments
def my_decorator(func):
    def wrapper():  # No parameters!
        return func()
    return wrapper

@my_decorator
def add(a, b):
    return a + b

add(2, 3)  # TypeError: wrapper() takes 0 arguments

# CORRECT - Accept any arguments
def my_decorator(func):
    def wrapper(*args, **kwargs):
        return func(*args, **kwargs)
    return wrapper
```

**4. Confusing Decorator vs Decorator Factory**
```python
# WRONG - Using parentheses when not needed
@my_decorator()  # Error if my_decorator doesn't return a decorator
def greet():
    pass

# CORRECT - No parentheses for simple decorator
@my_decorator
def greet():
    pass

# CORRECT - Parentheses only for decorator factories
@repeat(times=3)  # repeat() returns the actual decorator
def greet():
    pass
```

**5. Not Returning the Original Function's Result**
```python
# WRONG - Wrapper doesn't return the result
def my_decorator(func):
    @wraps(func)
    def wrapper(*args, **kwargs):
        print("Before")
        func(*args, **kwargs)  # Result is lost!
        print("After")
    return wrapper

@my_decorator
def add(a, b):
    return a + b

result = add(2, 3)  # Returns None!

# CORRECT - Return the function's result
def my_decorator(func):
    @wraps(func)
    def wrapper(*args, **kwargs):
        print("Before")
        result = func(*args, **kwargs)
        print("After")
        return result  # Return the result!
    return wrapper
```
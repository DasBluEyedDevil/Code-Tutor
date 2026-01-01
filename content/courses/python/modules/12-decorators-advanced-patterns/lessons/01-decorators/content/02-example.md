---
type: "EXAMPLE"
title: "Code Example: Understanding Decorators"
---

**How decorators work:**

1. **Decorator function:**
```python
def my_decorator(func):
    def wrapper(*args, **kwargs):
        # Before function call
        result = func(*args, **kwargs)
        # After function call
        return result
    return wrapper
```

2. **@ syntax:**
```python
@my_decorator
def my_func():
    pass
```

3. **Stacking:**
- Applied bottom-to-top
- `@timer @log @validate` means: validate → log → timer

4. **@wraps(func):**
- Preserves original function's name, docstring
- From functools module

```python
import time
from functools import wraps

# Simple decorator without arguments
def timer_decorator(func):
    """Measures function execution time"""
    @wraps(func)  # Preserves original function metadata
    def wrapper(*args, **kwargs):
        start = time.time()
        result = func(*args, **kwargs)
        end = time.time()
        print(f"{func.__name__} took {end - start:.4f} seconds")
        return result
    return wrapper

def log_decorator(func):
    """Logs function calls"""
    @wraps(func)
    def wrapper(*args, **kwargs):
        print(f"Calling {func.__name__} with args={args}, kwargs={kwargs}")
        result = func(*args, **kwargs)
        print(f"{func.__name__} returned: {result}")
        return result
    return wrapper

def validate_positive(func):
    """Validates that all arguments are positive numbers"""
    @wraps(func)
    def wrapper(*args, **kwargs):
        for arg in args:
            if isinstance(arg, (int, float)) and arg < 0:
                raise ValueError(f"All arguments must be positive, got {arg}")
        return func(*args, **kwargs)
    return wrapper

# Using decorators
print("=== Timer Decorator ===")

@timer_decorator
def slow_function():
    """Simulates slow operation"""
    time.sleep(0.1)
    return "Done!"

result = slow_function()
print(f"Result: {result}\n")

print("=== Log Decorator ===")

@log_decorator
def add(a, b):
    return a + b

result = add(5, 3)
print()

print("=== Validation Decorator ===")

@validate_positive
def calculate_area(width, height):
    return width * height

print(f"Area (5, 10): {calculate_area(5, 10)}")

try:
    calculate_area(-5, 10)
except ValueError as e:
    print(f"Error: {e}")

print("\n=== Stacking Decorators ===")

@timer_decorator
@log_decorator
@validate_positive
def multiply(a, b):
    return a * b

print("Calling multiply(4, 7):")
result = multiply(4, 7)

print("\n=== Without Decorator Syntax ===")
def divide(a, b):
    return a / b

# Manual decoration (equivalent to @decorator)
divide_logged = log_decorator(divide)
result = divide_logged(10, 2)
```

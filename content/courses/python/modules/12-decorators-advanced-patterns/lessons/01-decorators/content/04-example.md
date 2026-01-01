---
type: "EXAMPLE"
title: "Code Example: Decorators with Arguments"
---

**Decorator with arguments pattern:**

```python
def decorator_with_args(arg1, arg2):
    def decorator(func):
        def wrapper(*args, **kwargs):
            # Use arg1, arg2 here
            return func(*args, **kwargs)
        return wrapper
    return decorator
```

**Three levels:**
1. Outer function: takes decorator arguments
2. Middle function: takes the function to decorate
3. Inner wrapper: the actual wrapper

**Class-based decorators:**
- Use `__init__` to receive function
- Use `__call__` to make instance callable
- Can maintain state (like call count)

```python
from functools import wraps
import time

# Decorator factory (takes arguments)
def repeat(times):
    """Repeats function execution N times"""
    def decorator(func):
        @wraps(func)
        def wrapper(*args, **kwargs):
            result = None
            for i in range(times):
                print(f"  Execution {i+1}/{times}")
                result = func(*args, **kwargs)
            return result
        return wrapper
    return decorator

def retry(max_attempts=3, delay=1):
    """Retries function on exception"""
    def decorator(func):
        @wraps(func)
        def wrapper(*args, **kwargs):
            for attempt in range(max_attempts):
                try:
                    return func(*args, **kwargs)
                except Exception as e:
                    if attempt == max_attempts - 1:
                        raise
                    print(f"Attempt {attempt + 1} failed: {e}. Retrying in {delay}s...")
                    time.sleep(delay)
        return wrapper
    return decorator

def cache_result(func):
    """Simple caching decorator"""
    cached = {}
    @wraps(func)
    def wrapper(*args):
        if args in cached:
            print(f"  Cache hit for {args}")
            return cached[args]
        print(f"  Computing for {args}")
        result = func(*args)
        cached[args] = result
        return result
    return wrapper

print("=== Repeat Decorator ===")

@repeat(times=3)
def greet(name):
    print(f"  Hello, {name}!")

greet("Alice")

print("\n=== Retry Decorator ===")

attempt_count = 0

@retry(max_attempts=3, delay=0.5)
def unreliable_function():
    global attempt_count
    attempt_count += 1
    if attempt_count < 3:
        raise ConnectionError("Network error")
    return "Success!"

result = unreliable_function()
print(f"Final result: {result}")

print("\n=== Cache Decorator ===")

@cache_result
def fibonacci(n):
    if n < 2:
        return n
    return fibonacci(n-1) + fibonacci(n-2)

print("First call:")
result = fibonacci(5)
print(f"fibonacci(5) = {result}")

print("\nSecond call (should use cache):")
result = fibonacci(5)
print(f"fibonacci(5) = {result}")

print("\n=== Class-Based Decorator ===")

class CountCalls:
    """Decorator that counts function calls"""
    def __init__(self, func):
        self.func = func
        self.count = 0
        self.__name__ = func.__name__
    
    def __call__(self, *args, **kwargs):
        self.count += 1
        print(f"[Call #{self.count}] {self.func.__name__}")
        return self.func(*args, **kwargs)
    
    def reset_count(self):
        self.count = 0

@CountCalls
def process_data(data):
    return f"Processed: {data}"

process_data("A")
process_data("B")
process_data("C")
print(f"Total calls: {process_data.count}")
```

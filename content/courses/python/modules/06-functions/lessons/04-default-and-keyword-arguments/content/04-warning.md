---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Mutable Default Arguments (The #1 Python Gotcha!)**
```python
# WRONG - The list is shared between ALL calls!
def add_item(item, items=[]):
    items.append(item)
    return items

print(add_item("a"))  # ['a']
print(add_item("b"))  # ['a', 'b'] - Unexpected!

# CORRECT - Use None and create inside function
def add_item(item, items=None):
    if items is None:
        items = []
    items.append(item)
    return items
```

**2. Putting Default Parameters Before Required Parameters**
```python
# WRONG - SyntaxError!
def greet(greeting="Hello", name):
    print(f"{greeting}, {name}!")

# CORRECT - Required parameters first
def greet(name, greeting="Hello"):
    print(f"{greeting}, {name}!")
```

**3. Putting Keyword Arguments Before Positional Arguments**
```python
# WRONG - SyntaxError!
create_user(name="Alice", 30, "NYC")

# CORRECT - Positional arguments first
create_user("Alice", 30, city="NYC")
create_user("Alice", age=30, city="NYC")  # Also valid
```

**4. Using the Same Parameter Name Twice**
```python
# WRONG - TypeError: got multiple values
def greet(name, greeting="Hello"):
    print(f"{greeting}, {name}!")

greet("Alice", name="Bob")  # 'name' specified twice!

# CORRECT - Use either positional OR keyword, not both
greet("Alice", greeting="Hi")
greet(name="Alice", greeting="Hi")
```

**5. Assuming Default Values Are Re-evaluated Each Call**
```python
import time

# WRONG - timestamp is set ONCE when function is defined
def log_message(msg, timestamp=time.time()):
    print(f"[{timestamp}] {msg}")

# CORRECT - Use None and get timestamp inside
def log_message(msg, timestamp=None):
    if timestamp is None:
        timestamp = time.time()
    print(f"[{timestamp}] {msg}")
```
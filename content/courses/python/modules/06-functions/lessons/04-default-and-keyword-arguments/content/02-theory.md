---
type: "THEORY"
title: "Default Arguments and Keyword Arguments"
---

**Default Arguments** - Set in the function definition:

```python
def greet(name, greeting="Hello"):
    print(f"{greeting}, {name}!")

greet("Alice")              # Hello, Alice!
greet("Bob", "Hi")          # Hi, Bob!
greet("Charlie", "Hey")     # Hey, Charlie!
```

**Rule**: Parameters with defaults must come AFTER parameters without defaults!

```python
# CORRECT
def greet(name, greeting="Hello"):
    ...

# ERROR! - Default before non-default
def greet(greeting="Hello", name):
    ...
```

**Keyword Arguments** - Specify by name when calling:

```python
def create_user(name, age, city, active=True):
    print(f"{name}, {age}, from {city}, active: {active}")

# Positional arguments (order matters)
create_user("Alice", 30, "NYC")

# Keyword arguments (order doesn't matter!)
create_user(city="LA", name="Bob", age=25)

# Mix positional and keyword (positional first!)
create_user("Charlie", age=35, city="Chicago", active=False)
```

**Keyword arguments are especially useful when:**
- Functions have many parameters
- You want to skip some default parameters
- You want to make code more readable
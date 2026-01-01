---
type: "EXAMPLE"
title: "Code Example: Basic Type Hints"
---

**Type hint syntax:**

**Function annotations:**
```python
def function(param: type) -> return_type:
    pass
```

**Variable annotations:**
```python
variable: type = value
```

**Important notes:**

1. **Not enforced at runtime:**
   ```python
   def add(a: int, b: int) -> int:
       return a + b
   
   add("hello", "world")  # Works! No error at runtime
   ```

2. **Need type checker:**
   - Use `mypy` or similar tool
   - Checks types before running
   - IDE integration

3. **Modern syntax (Python 3.10+):**
   ```python
   # Old
   Optional[str]  → str | None
   Union[int, str] → int | str
   ```

```python
print("=== Basic Type Hints ===")

# Simple function with type hints
def greet(name: str) -> str:
    """Return a greeting message"""
    return f"Hello, {name}!"

result = greet("Alice")
print(result)

# Multiple parameters
def add_numbers(a: int, b: int) -> int:
    """Add two integers"""
    return a + b

print(f"Sum: {add_numbers(5, 3)}")

# Default values with type hints
def create_user(name: str, age: int = 18, active: bool = True) -> dict:
    """Create user dictionary"""
    return {'name': name, 'age': age, 'active': active}

user = create_user("Bob", 25)
print(f"User: {user}")

print("\n=== Collection Type Hints ===")

# List of specific type
def process_names(names: list[str]) -> list[str]:
    """Convert names to uppercase"""
    return [name.upper() for name in names]

names = ["alice", "bob", "charlie"]
print(f"Uppercase: {process_names(names)}")

# Dictionary with type hints
def count_words(text: str) -> dict[str, int]:
    """Count word frequency"""
    words = text.split()
    return {word: words.count(word) for word in set(words)}

result = count_words("hello world hello")
print(f"Word count: {result}")

# Tuple with specific types
def get_user_info(user_id: int) -> tuple[str, int, str]:
    """Return (name, age, email)"""
    return ("Alice", 25, "alice@example.com")

name, age, email = get_user_info(1)
print(f"User: {name}, {age}, {email}")

print("\n=== Optional and None ===")

from typing import Optional

# Optional means "can be None"
def find_user(user_id: int) -> Optional[dict]:
    """Find user by ID, return None if not found"""
    if user_id == 1:
        return {'id': 1, 'name': 'Alice'}
    return None

user = find_user(1)
print(f"Found: {user}")

user = find_user(999)
print(f"Not found: {user}")

# Modern Python 3.10+ can use | None
def find_product(product_id: int) -> dict | None:
    """Find product, None if not found"""
    return None

print("\n=== Union Types ===")

from typing import Union

# Can be multiple types
def process_id(user_id: Union[int, str]) -> str:
    """Accept int or string ID"""
    return f"ID: {user_id}"

print(process_id(123))
print(process_id("ABC123"))

# Modern Python 3.10+ can use |
def format_value(value: int | float | str) -> str:
    """Format any of these types"""
    return f"Value: {value}"

print(format_value(42))
print(format_value(3.14))
print(format_value("text"))

print("\n=== Variable Annotations ===")

# Annotate variables
name: str = "Alice"
age: int = 25
scores: list[int] = [85, 90, 92]
config: dict[str, bool] = {'debug': True, 'verbose': False}

print(f"Name: {name}, Age: {age}")
print(f"Scores: {scores}")
print(f"Config: {config}")

# Type hints don't prevent wrong types at runtime!
name = 123  # No error! Type hints are not enforced
print(f"Name is now: {name} (still works, but type checkers would complain)")
```

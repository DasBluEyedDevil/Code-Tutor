---
type: "THEORY"
title: "Syntax Breakdown"
---

### Basic Type Hint Syntax:

**Function Parameters:**
```python
def function_name(param: type) -> return_type:
    ...
```

**Variable Annotations:**
```python
variable: type = value
```

### Common Built-in Types:

| Type | Example | Description |
|------|---------|-------------|
| `str` | `name: str = "Alice"` | Text strings |
| `int` | `age: int = 25` | Whole numbers |
| `float` | `price: float = 19.99` | Decimal numbers |
| `bool` | `active: bool = True` | True/False values |
| `None` | `-> None` | Function returns nothing |

### Collection Types (Python 3.9+):

```python
# Lists with element type
numbers: list[int] = [1, 2, 3]
names: list[str] = ["Alice", "Bob"]

# Dictionaries with key and value types
grades: dict[str, int] = {"Alice": 95, "Bob": 87}
config: dict[str, str] = {"host": "localhost"}

# Sets with element type
unique_ids: set[int] = {1, 2, 3}

# Tuples with specific types for each position
point: tuple[int, int] = (10, 20)
person: tuple[str, int, bool] = ("Alice", 25, True)
```

### Modern Type Syntax (Python 3.10+):

**Optional - Value or None:**
```python
# Modern syntax - no import needed!
name: str | None = None

# Old style (avoid in new code):
# from typing import Optional
# name: Optional[str] = None
```

**Union - Multiple Possible Types:**
```python
# Modern syntax - no import needed!
id_value: str | int = "abc"

# Old style (avoid in new code):
# from typing import Union
# id_value: Union[str, int] = "abc"
```

**Any - Accepts Any Type:**
```python
from typing import Any

def log_anything(value: Any) -> None:
    print(value)
```

### Function Return Types:

```python
# Returns a value
def add(a: int, b: int) -> int:
    return a + b

# Returns nothing (None)
def print_greeting(name: str) -> None:
    print(f"Hello, {name}")

# Returns a list
def get_names() -> list[str]:
    return ["Alice", "Bob"]

# Returns value or None
def find_user(user_id: int) -> dict | None:
    return users.get(user_id)
```

### Type Hints Don't Enforce!

```python
def greet(name: str) -> str:
    return f"Hello, {name}"

# This RUNS without error (Python ignores type hints at runtime)
result = greet(42)  # Type hint says str, but we passed int

# But tools like mypy catch this:
# $ mypy script.py
# error: Argument 1 has incompatible type "int"; expected "str"
```

### Best Practices:

1. **Use type hints for public APIs** - Functions others will use
2. **Be specific with collections** - `list[str]` not just `list`
3. **Use X | None for nullable values** - Cleaner than Optional[X]
4. **Return type is important** - Helps IDE provide better autocomplete
5. **Don't over-annotate** - Simple internal functions may not need hints
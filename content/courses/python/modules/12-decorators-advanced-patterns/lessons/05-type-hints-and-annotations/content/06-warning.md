---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using Mutable Default Arguments with Type Hints**
```python
# WRONG - Mutable default shared across calls
def add_item(item: str, items: list[str] = []) -> list[str]:
    items.append(item)
    return items

# CORRECT - Use None and create new list
def add_item(item: str, items: list[str] | None = None) -> list[str]:
    if items is None:
        items = []
    items.append(item)
    return items
```

**2. Confusing list vs List (Python < 3.9)**
```python
# WRONG on Python < 3.9
def process(items: list[str]) -> dict[str, int]:  # TypeError!
    pass

# CORRECT for Python < 3.9 - use typing module
from typing import List, Dict
def process(items: List[str]) -> Dict[str, int]:
    pass
```

**3. Expecting Runtime Type Checking**
```python
# WRONG - Type hints don't enforce at runtime
def greet(name: str) -> str:
    return f'Hello, {name}'

greet(123)  # No error! Returns 'Hello, 123'

# CORRECT - Add runtime validation if needed
def greet(name: str) -> str:
    if not isinstance(name, str):
        raise TypeError('name must be a string')
    return f'Hello, {name}'
```

**4. Wrong Optional Syntax**
```python
# WRONG - Optional means 'can be None', not optional param
from typing import Optional
def greet(name: Optional[str]) -> str:  # Still required!
    return f'Hello, {name}'

greet()  # TypeError: missing argument 'name'

# CORRECT - Add default value
def greet(name: str | None = None) -> str:
    return f'Hello, {name or "stranger"}'
```

**5. Forward Reference Without Quotes**
```python
# WRONG - Class not defined yet
class Node:
    def get_next(self) -> Node:  # NameError!
        pass

# CORRECT - Use string for forward reference
class Node:
    def get_next(self) -> 'Node':  # Works!
        pass
```
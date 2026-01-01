---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Thinking Type Hints Enforce Types at Runtime**
```python
# WRONG - Assuming Python will reject this
def greet(name: str) -> str:
    return f"Hello, {name}"

greet(123)  # This RUNS without error! Python ignores hints at runtime

# CORRECT - Type hints are for documentation and static checkers only
# Use tools like mypy to catch type errors BEFORE running
# $ mypy script.py  # Catches the error
```

**2. Using Capitalized List/Dict Instead of Lowercase (Python 3.9+)**
```python
# WRONG - Old style, requires import
from typing import List, Dict
def process(items: List[str]) -> Dict[str, int]:
    pass

# CORRECT - Python 3.9+ uses lowercase built-in types
def process(items: list[str]) -> dict[str, int]:
    pass
```

**3. Using Old Optional Syntax Instead of X | None**
```python
# OLD STYLE - Works but verbose
from typing import Optional
def greet(name: Optional[str] = None):
    pass

# MODERN (Python 3.10+) - Cleaner, no import needed
def greet(name: str | None = None):  # Preferred!
    pass
```

**4. Confusing -> None with No Return Type**
```python
# WRONG - Omitting return type (unclear intention)
def print_greeting(name: str):
    print(f"Hello, {name}")

# CORRECT - Explicitly show function returns nothing
def print_greeting(name: str) -> None:
    print(f"Hello, {name}")
```

**5. Over-Using Any Type**
```python
# WRONG - Using Any defeats the purpose of type hints
from typing import Any
def process(data: Any) -> Any:  # This tells us nothing!
    return data

# CORRECT - Be specific about types
def process(data: dict[str, int]) -> list[int]:
    return list(data.values())
```
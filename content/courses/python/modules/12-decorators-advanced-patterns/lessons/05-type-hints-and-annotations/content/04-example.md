---
type: "EXAMPLE"
title: "Code Example: Advanced Type Hints"
---

**Advanced type hint concepts:**

**1. Any:**
- Accepts any type
- Opts out of type checking
- Use sparingly

**2. Callable:**
```python
Callable[[arg1_type, arg2_type], return_type]
Callable[[], None]  # No args, no return
```

**3. Type aliases:**
```python
UserId = int
UserDict = dict[str, Any]
```

**4. TypeVar (generics):**
```python
T = TypeVar('T')
def func(x: T) -> T:  # Same type in and out
    return x
```

**5. Self-referencing:**
```python
class Node:
    def get_parent(self) -> 'Node | None':
        ...
```
Use quotes for forward references

**Type checking tools:**
- mypy: `mypy script.py`
- pyright: Built into VS Code
- pyre: Facebook's type checker

```python
from typing import Any, Callable, TypeVar, Generic

print("=== Any Type ===")

def process_data(data: Any) -> Any:
    """Accept and return any type"""
    return data

print(process_data(123))
print(process_data("text"))
print(process_data([1, 2, 3]))

print("\n=== Callable (Function) Types ===")

def apply_operation(value: int, operation: Callable[[int], int]) -> int:
    """Apply a function to a value"""
    return operation(value)

def double(x: int) -> int:
    return x * 2

def square(x: int) -> int:
    return x ** 2

print(f"Double 5: {apply_operation(5, double)}")
print(f"Square 5: {apply_operation(5, square)}")

# Lambda with type hints (in context)
result = apply_operation(10, lambda x: x + 1)
print(f"Add 1 to 10: {result}")

print("\n=== Type Aliases ===")

# Create type aliases for complex types
Vector = list[float]
Matrix = list[list[float]]
JSONDict = dict[str, Any]

def add_vectors(v1: Vector, v2: Vector) -> Vector:
    """Add two vectors"""
    return [a + b for a, b in zip(v1, v2)]

vec1: Vector = [1.0, 2.0, 3.0]
vec2: Vector = [4.0, 5.0, 6.0]
result = add_vectors(vec1, vec2)
print(f"Vector sum: {result}")

print("\n=== Class Type Hints ===")

class User:
    def __init__(self, name: str, age: int):
        self.name: str = name
        self.age: int = age
    
    def get_info(self) -> str:
        """Return user info"""
        return f"{self.name} ({self.age})"
    
    @classmethod
    def from_dict(cls, data: dict[str, Any]) -> 'User':
        """Create User from dictionary"""
        return cls(data['name'], data['age'])
    
    def is_adult(self) -> bool:
        """Check if user is adult"""
        return self.age >= 18

user = User("Alice", 25)
print(user.get_info())

user2 = User.from_dict({'name': 'Bob', 'age': 30})
print(f"Is Bob adult? {user2.is_adult()}")

print("\n=== Generic Types ===")

T = TypeVar('T')  # Generic type variable

def get_first(items: list[T]) -> T | None:
    """Get first item from list"""
    return items[0] if items else None

# Works with any type
print(f"First int: {get_first([1, 2, 3])}")
print(f"First str: {get_first(['a', 'b', 'c'])}")
print(f"Empty: {get_first([])}")

def swap_pair(a: T, b: T) -> tuple[T, T]:
    """Swap two values of same type"""
    return b, a

print(f"Swap ints: {swap_pair(1, 2)}")
print(f"Swap strs: {swap_pair('hello', 'world')}")

print("\n=== Practical Example: Typed Data Processing ===")

from typing import Iterator

def read_numbers(filename: str) -> Iterator[int]:
    """Read numbers from file, one per line"""
    with open(filename) as f:
        for line in f:
            yield int(line.strip())

def calculate_stats(numbers: list[int]) -> dict[str, float]:
    """Calculate statistics"""
    return {
        'mean': sum(numbers) / len(numbers),
        'min': float(min(numbers)),
        'max': float(max(numbers))
    }

# Create test file
with open('numbers.txt', 'w') as f:
    f.write('10\n20\n30\n40\n50')

numbers = list(read_numbers('numbers.txt'))
stats = calculate_stats(numbers)
print(f"Numbers: {numbers}")
print(f"Stats: {stats}")

print("\n=== Type Checking Example ===")

def process_config(config: dict[str, str | int | bool]) -> None:
    """Process configuration"""
    for key, value in config.items():
        print(f"{key}: {value} ({type(value).__name__})")

config = {
    'host': 'localhost',
    'port': 8080,
    'debug': True,
    'timeout': 30
}

process_config(config)

import os
if os.path.exists('numbers.txt'):
    os.remove('numbers.txt')
```

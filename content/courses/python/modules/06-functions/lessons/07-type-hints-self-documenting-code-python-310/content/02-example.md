---
type: "EXAMPLE"
title: "Code Example: Basic Type Hints"
---

**Expected Output:**
```
=== Basic Type Hints ===
Hello, Alice!
Circle area: 78.54
Is 18 an adult? True

=== Variable Annotations ===
Name: Bob, Age: 25, Height: 5.9, Active: True

=== Collection Types ===
Sum: 15
Highest grade: 95
Formatted names: ALICE, BOB, CHARLIE

=== Optional Types ===
No middle name provided
Full name with middle: John Paul Doe

=== Union Types ===
String ID: user_123
Numeric ID: 00042
Processed: $99.99
Processed: 100
```

```python
# Type Hints: Making Python Code Self-Documenting
# Python 3.10+ syntax - no imports needed for basic types!

print("=== Basic Type Hints ===")

# Basic function with type hints
# name: str means 'name should be a string'
# -> str means 'this function returns a string'
def greet(name: str) -> str:
    return f"Hello, {name}!"

# Function with multiple typed parameters
def calculate_area(radius: float) -> float:
    pi = 3.14159
    return pi * radius * radius

# Function returning a boolean
def is_adult(age: int) -> bool:
    return age >= 18

print(greet("Alice"))
print(f"Circle area: {calculate_area(5.0):.2f}")
print(f"Is 18 an adult? {is_adult(18)}")

print()
print("=== Variable Annotations ===")

# Variable type annotations
name: str = "Bob"
age: int = 25
height: float = 5.9
is_active: bool = True

print(f"Name: {name}, Age: {age}, Height: {height}, Active: {is_active}")

print()
print("=== Collection Types ===")

# Collection types (Python 3.9+ syntax)
def sum_numbers(numbers: list[int]) -> int:
    total = 0
    for num in numbers:
        total += num
    return total

def get_highest_grade(grades: dict[str, int]) -> int:
    return max(grades.values())

def format_names(names: list[str]) -> list[str]:
    return [name.upper() for name in names]

print(f"Sum: {sum_numbers([1, 2, 3, 4, 5])}")
print(f"Highest grade: {get_highest_grade({'Alice': 95, 'Bob': 87, 'Charlie': 92})}")
print(f"Formatted names: {', '.join(format_names(['Alice', 'Bob', 'Charlie']))}")

print()
print("=== Optional Types (Python 3.10+) ===")

# Use X | None instead of Optional[X] - cleaner, no import needed!
def get_full_name(first: str, last: str, middle: str | None = None) -> str:
    if middle:
        return f"{first} {middle} {last}"
    return f"{first} {last}"

print(get_full_name("John", "Doe"))  # No middle name
print(f"Full name with middle: {get_full_name('John', 'Doe', 'Paul')}")

print()
print("=== Union Types (Python 3.10+) ===")

# Use | for union types - cleaner than Union[X, Y]
def process_id(user_id: str | int) -> str:
    if isinstance(user_id, int):
        return f"{user_id:05d}"  # Pad with zeros
    return user_id

def format_price(price: int | float) -> str:
    if isinstance(price, float):
        return f"${price:.2f}"
    return str(price)

print(f"String ID: {process_id('user_123')}")
print(f"Numeric ID: {process_id(42)}")
print(f"Processed: {format_price(99.99)}")
print(f"Processed: {format_price(100)}")
```

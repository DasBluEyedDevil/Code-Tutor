---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Validating After Using the Value**
```python
# WRONG - Using value before validation
def process_age(age):
    birth_year = 2024 - age  # Used before validation!
    if age < 0 or age > 150:
        raise ValueError("Invalid age")
    return birth_year

# CORRECT - Validate first, then use
def process_age(age):
    if age < 0 or age > 150:
        raise ValueError("Invalid age")
    birth_year = 2024 - age
    return birth_year
```

**2. Not Validating Type Before Value**
```python
# WRONG - Value check fails on wrong type
def set_count(count):
    if count < 0:  # TypeError if count is string!
        raise ValueError("Count must be positive")

# CORRECT - Check type first
def set_count(count):
    if not isinstance(count, int):
        raise TypeError(f"Expected int, got {type(count).__name__}")
    if count < 0:
        raise ValueError("Count must be positive")
```

**3. Trusting Input Without Sanitization**
```python
# WRONG - User input used directly
filename = input("Enter filename: ")
with open(filename) as f:  # Path traversal vulnerability!
    data = f.read()

# CORRECT - Sanitize and validate input
import os
filename = input("Enter filename: ")
safe_name = os.path.basename(filename)  # Remove path components
if not safe_name.endswith('.txt'):
    raise ValueError("Only .txt files allowed")
```

**4. Using assert for Input Validation**
```python
# WRONG - assert can be disabled with -O flag
def withdraw(amount):
    assert amount > 0, "Amount must be positive"  # Skipped with python -O!
    return balance - amount

# CORRECT - Use explicit validation
def withdraw(amount):
    if amount <= 0:
        raise ValueError("Amount must be positive")
    return balance - amount
```

**5. Inconsistent Validation Across Functions**
```python
# WRONG - Different validation in different places
def create_user(age):
    if age < 18:
        raise ValueError("Must be 18+")

def update_user(age):
    if age < 0:  # Different rule!
        raise ValueError("Invalid age")

# CORRECT - Centralize validation
def validate_age(age, min_age=0):
    if not isinstance(age, int) or age < min_age:
        raise ValueError(f"Age must be >= {min_age}")

def create_user(age):
    validate_age(age, min_age=18)
```
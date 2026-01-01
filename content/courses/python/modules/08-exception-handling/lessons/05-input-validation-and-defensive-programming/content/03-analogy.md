---
type: "ANALOGY"
title: "Syntax Breakdown: Validation Patterns"
---

**Input Validation Checklist:**

**1. Check for empty/null:**
```python
if not value or value.strip() == "":
    raise ValueError("Input cannot be empty")
```

**2. Type validation:**
```python
# Check if value is the right type
if not isinstance(value, int):
    raise TypeError(f"Expected int, got {type(value).__name__}")

# Accept multiple types
if not isinstance(price, (int, float)):
    raise TypeError("Price must be a number")
```

**3. Range validation:**
```python
# Check numeric range
if not 0 <= age <= 120:
    raise ValueError(f"Age must be 0-120, got {age}")

# Check string length
if len(password) < 8:
    raise ValueError("Password must be at least 8 characters")
```

**4. Format validation:**
```python
# Email contains @
if "@" not in email:
    raise ValueError("Invalid email format")

# String is numeric
if not value.isdigit():
    raise ValueError("Must contain only digits")
```

**5. Sanitization (clean the input):**
```python
# Remove whitespace
value = value.strip()

# Convert to lowercase for comparison
value = value.lower()

# Remove dangerous characters
value = value.replace(";", "").replace("--", "")
```

**EAFP vs LBYL:**

**LBYL (Look Before You Leap):**
```python
# Check condition first, then act
if key in dictionary:
    value = dictionary[key]
else:
    value = default

if os.path.exists(filename):
    with open(filename) as f:
        data = f.read()
```

**EAFP (Easier to Ask Forgiveness than Permission):**
```python
# Try it, handle errors if they occur
try:
    value = dictionary[key]
except KeyError:
    value = default

try:
    with open(filename) as f:
        data = f.read()
except FileNotFoundError:
    data = None
```

**When to use each:**

**Use LBYL when:**
- Simple conditions (if key in dict)
- Readability matters more than performance
- You want to avoid exceptions in common cases

**Use EAFP when:**
- The success case is more common than failure
- Race conditions possible (file might be deleted between check and use)
- More Pythonic ("ask forgiveness")

**Defensive Programming Best Practices:**

```python
def robust_function(param1, param2=None):
    """Template for defensive programming."""
    
    # 1. Validate all parameters
    if param1 is None:
        raise ValueError("param1 is required")
    
    if not isinstance(param1, str):
        raise TypeError("param1 must be a string")
    
    # 2. Sanitize input
    param1 = param1.strip()
    
    # 3. Check business rules
    if len(param1) < 3:
        raise ValueError("param1 must be at least 3 characters")
    
    # 4. Handle optional parameters
    if param2 is None:
        param2 = default_value
    
    # 5. Use try/except for risky operations
    try:
        result = risky_operation(param1)
    except SomeError as e:
        # Log and handle
        logger.error(f"Operation failed: {e}")
        return None
    
    return result
```

**Type hints (Python 3.5+) help with validation:**
```python
def process_user(name: str, age: int) -> dict:
    """Type hints document expected types."""
    # Still need runtime validation!
    if not isinstance(name, str):
        raise TypeError("name must be str")
    return {"name": name, "age": age}
```
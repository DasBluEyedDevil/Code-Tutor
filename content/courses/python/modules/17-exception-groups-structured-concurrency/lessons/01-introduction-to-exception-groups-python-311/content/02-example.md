---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Caught exception group: Multiple validation errors (3 sub-exceptions)
  - Invalid email format
  - Password too short
  - Username contains invalid characters
```

```python
# Creating and raising an ExceptionGroup

class ValidationError(Exception):
    pass

def validate_user(email: str, password: str, username: str):
    errors = []
    
    if "@" not in email:
        errors.append(ValidationError("Invalid email format"))
    
    if len(password) < 8:
        errors.append(ValidationError("Password too short"))
    
    if not username.isalnum():
        errors.append(ValidationError("Username contains invalid characters"))
    
    if errors:
        raise ExceptionGroup("Multiple validation errors", errors)
    
    return {"email": email, "password": password, "username": username}

# Usage
try:
    validate_user("bademail", "short", "user@name")
except ExceptionGroup as eg:
    print(f"Caught exception group: {eg.message} ({len(eg.exceptions)} sub-exceptions)")
    for exc in eg.exceptions:
        print(f"  - {exc}")

```

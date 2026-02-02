---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Handling ValueError: Invalid email
Handling ValueError: Invalid phone
Handling TypeError: Expected string, got int
Unhandled errors remain: 1 sub-exceptions
```

```python
# Using except* to handle different exception types

def process_data():
    raise ExceptionGroup("Multiple errors", [
        ValueError("Invalid email"),
        ValueError("Invalid phone"),
        TypeError("Expected string, got int"),
        KeyError("Missing 'name' field")
    ])

try:
    process_data()
except* ValueError as eg:
    # Handles ALL ValueErrors in the group
    for exc in eg.exceptions:
        print(f"Handling ValueError: {exc}")
except* TypeError as eg:
    # Handles ALL TypeErrors in the group
    for exc in eg.exceptions:
        print(f"Handling TypeError: {exc}")
except* KeyError as eg:
    # KeyError is not handled here, so it propagates
    print(f"Unhandled errors remain: {len(eg.exceptions)} sub-exceptions")

```

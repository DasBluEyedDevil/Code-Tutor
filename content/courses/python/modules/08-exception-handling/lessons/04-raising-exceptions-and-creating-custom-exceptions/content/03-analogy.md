---
type: "ANALOGY"
title: "Syntax Breakdown: Raising and Creating Exceptions"
---

**Raising Built-in Exceptions:**

```python
# Basic syntax
raise ExceptionType("Error message")

# Examples
raise ValueError("Age cannot be negative")
raise TypeError("Expected string, got int")
raise FileNotFoundError("Config file not found")

# Without message (less helpful)
raise ValueError()
```

**Creating Custom Exception Classes:**

**Basic custom exception:**
```python
class MyCustomError(Exception):
    """Description of when this error occurs."""
    pass  # No additional code needed

# Usage
raise MyCustomError("Something specific went wrong")
```

**Custom exception with default message:**
```python
class InsufficientFundsError(Exception):
    """Raised when account balance is too low."""
    def __init__(self, balance, amount):
        self.balance = balance
        self.amount = amount
        message = f"Insufficient funds: ${balance} < ${amount}"
        super().__init__(message)

# Usage
raise InsufficientFundsError(balance=100, amount=200)
```

**Exception hierarchy for related errors:**
```python
class BankError(Exception):
    """Base exception for all bank errors."""
    pass

class InsufficientFundsError(BankError):
    """Not enough money."""
    pass

class AccountLockedError(BankError):
    """Account is locked."""
    pass

# Can catch all bank errors:
try:
    do_banking()
except BankError:  # Catches both children
    handle_bank_error()

# Or catch specific ones:
except InsufficientFundsError:
    handle_insufficient_funds()
```

**Re-raising Exceptions:**

**Bare raise (re-raises the same exception):**
```python
try:
    risky_operation()
except ValueError as e:
    log_error(e)  # Log it
    raise  # Re-raise the original exception
```

**Raising a different exception:**
```python
try:
    risky_operation()
except ValueError as e:
    # Convert to custom exception
    raise CustomError(f"Failed: {e}")
```

**When to Raise Exceptions:**

✅ **DO raise exceptions when:**
- Input validation fails (negative age, invalid email)
- Preconditions not met (function expects positive number, got zero)
- Business rules violated (withdrawal exceeds limit)
- Unrecoverable errors (file corrupted, database connection lost)

❌ **DON'T raise exceptions when:**
- Result could be legitimately empty (search returns 0 results)
- User input is commonly wrong (wrong password)
- You can return a sentinel value (None, False, -1)
- It's an expected alternate flow (user cancels operation)

**Guideline:** Exceptions are for EXCEPTIONAL cases, not control flow.
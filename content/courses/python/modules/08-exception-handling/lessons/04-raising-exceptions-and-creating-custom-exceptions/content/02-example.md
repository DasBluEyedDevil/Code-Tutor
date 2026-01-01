---
type: "EXAMPLE"
title: "Code Example: Raising Exceptions"
---

The code demonstrates:
1. **Raising built-in exceptions** with raise ValueError() when validation fails
2. **Custom exception classes** that inherit from Exception for domain-specific errors
3. **Re-raising exceptions** with bare raise to pass the exception up after logging

Custom exceptions make error handling more specific and self-documenting.

```python
# Example 1: Raising built-in exceptions
print("=== Raising Built-in Exceptions ===")

def calculate_discount(price, discount_percent):
    """Calculate discounted price with validation."""
    
    # Validate price
    if price < 0:
        raise ValueError("Price cannot be negative")
    
    # Validate discount
    if discount_percent < 0 or discount_percent > 100:
        raise ValueError("Discount must be between 0 and 100")
    
    # If validations pass, calculate discount
    discount_amount = price * (discount_percent / 100)
    final_price = price - discount_amount
    return final_price

# Valid usage
print("Test 1: Valid values")
try:
    result = calculate_discount(100, 20)
    print(f"$100 with 20% discount = ${result}\n")
except ValueError as e:
    print(f"Error: {e}\n")

# Invalid price
print("Test 2: Negative price")
try:
    result = calculate_discount(-50, 20)
    print(f"Result: ${result}\n")
except ValueError as e:
    print(f"Error: {e}\n")

# Invalid discount
print("Test 3: Invalid discount")
try:
    result = calculate_discount(100, 150)
    print(f"Result: ${result}\n")
except ValueError as e:
    print(f"Error: {e}\n")

# Example 2: Creating custom exceptions
print("=== Custom Exception Classes ===")

# Define custom exception
class InsufficientFundsError(Exception):
    """Raised when account has insufficient funds for withdrawal."""
    pass

class AccountLockedError(Exception):
    """Raised when attempting to access a locked account."""
    pass

class BankAccount:
    """Simple bank account with custom exceptions."""
    
    def __init__(self, balance=0):
        self.balance = balance
        self.is_locked = False
    
    def withdraw(self, amount):
        """Withdraw money with validation."""
        
        # Check if account is locked
        if self.is_locked:
            raise AccountLockedError("Account is locked. Contact bank.")
        
        # Validate amount
        if amount <= 0:
            raise ValueError("Withdrawal amount must be positive")
        
        # Check sufficient funds
        if amount > self.balance:
            raise InsufficientFundsError(
                f"Insufficient funds. Balance: ${self.balance}, "
                f"Requested: ${amount}"
            )
        
        # Process withdrawal
        self.balance -= amount
        return self.balance
    
    def lock_account(self):
        """Lock the account."""
        self.is_locked = True

# Test custom exceptions
account = BankAccount(balance=1000)

print("Test 1: Valid withdrawal")
try:
    new_balance = account.withdraw(200)
    print(f"Withdrew $200. New balance: ${new_balance}\n")
except (InsufficientFundsError, AccountLockedError, ValueError) as e:
    print(f"Error: {e}\n")

print("Test 2: Insufficient funds")
try:
    new_balance = account.withdraw(5000)
    print(f"New balance: ${new_balance}\n")
except InsufficientFundsError as e:
    print(f"Error: {e}\n")

print("Test 3: Locked account")
account.lock_account()
try:
    new_balance = account.withdraw(100)
    print(f"New balance: ${new_balance}\n")
except AccountLockedError as e:
    print(f"Error: {e}\n")

# Example 3: Re-raising exceptions
print("=== Re-raising Exceptions ===")

def process_transaction(account, amount):
    """Process transaction with logging."""
    try:
        account.withdraw(amount)
        print(f"Transaction successful: ${amount}")
    except InsufficientFundsError as e:
        print(f"[LOG] Failed transaction: {e}")
        raise  # Re-raise the same exception

account2 = BankAccount(balance=100)
try:
    process_transaction(account2, 200)
except InsufficientFundsError:
    print("Transaction declined at higher level")
```

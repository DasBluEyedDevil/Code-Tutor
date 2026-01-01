---
type: "EXAMPLE"
title: "Code Example: Properties and Encapsulation"
---

**@property decorator creates managed attributes:**

**1. Read-only property:**
```python
@property
def balance(self):
    return self.__balance
```
Usage: `account.balance` (looks like attribute, actually method)

**2. Property with setter:**
```python
@property
def fee(self):
    return self._fee

@fee.setter
def fee(self, value):
    if value < 0:
        raise ValueError()
    self._fee = value
```

**3. Write-only property:**
- Getter raises AttributeError
- Only setter works

**Key benefits:**
- Validation on setting
- Read-only access
- Computed values
- Looks like attribute access

```python
class BankAccount:
    def __init__(self, account_holder, initial_balance=0):
        self.account_holder = account_holder  # Public
        self._transaction_fee = 1.50          # Protected (by convention)
        self.__balance = initial_balance       # Private (name mangled)
        self.__pin = None                      # Private
    
    # Property: balance (read-only)
    @property
    def balance(self):
        """Read-only access to balance"""
        return self.__balance
    
    # Property: pin (write-only via setter)
    @property
    def pin(self):
        """Can't read PIN!"""
        raise AttributeError("PIN is write-only")
    
    @pin.setter
    def pin(self, value):
        """Set PIN with validation"""
        if not isinstance(value, str) or len(value) != 4 or not value.isdigit():
            raise ValueError("PIN must be 4 digits")
        self.__pin = value
        print("PIN set successfully")
    
    # Property with getter and setter
    @property
    def transaction_fee(self):
        return self._transaction_fee
    
    @transaction_fee.setter
    def transaction_fee(self, value):
        if value < 0:
            raise ValueError("Fee cannot be negative")
        self._transaction_fee = value
    
    # Methods that use private attributes
    def deposit(self, amount):
        if amount <= 0:
            raise ValueError("Deposit must be positive")
        self.__balance += amount - self._transaction_fee
        return f"Deposited ${amount}. Fee: ${self._transaction_fee}. New balance: ${self.__balance}"
    
    def withdraw(self, amount, pin):
        if pin != self.__pin:
            raise ValueError("Invalid PIN")
        if amount <= 0:
            raise ValueError("Withdrawal must be positive")
        total = amount + self._transaction_fee
        if total > self.__balance:
            raise ValueError("Insufficient funds")
        self.__balance -= total
        return f"Withdrew ${amount}. Fee: ${self._transaction_fee}. New balance: ${self.__balance}"

print("=== Creating Account ===")
account = BankAccount("Alice", 1000)

print("\n=== Reading Balance (Property) ===")
print(f"Balance: ${account.balance}")  # Uses @property

# Try to modify balance directly (won't work!)
print("\n=== Trying to Modify Balance Directly ===")
try:
    account.balance = 5000  # No setter defined!
except AttributeError as e:
    print(f"Error: {e}")

print("\n=== Setting PIN (Property with Validation) ===")
account.pin = "1234"  # Uses @pin.setter

# Invalid PINs
print("\n=== Invalid PIN Examples ===")
try:
    account.pin = "123"  # Too short
except ValueError as e:
    print(f"Error: {e}")

try:
    account.pin = "abcd"  # Not digits
except ValueError as e:
    print(f"Error: {e}")

print("\n=== Can't Read PIN ===")
try:
    print(account.pin)  # Raises AttributeError
except AttributeError as e:
    print(f"Error: {e}")

print("\n=== Transaction Fee Property ===")
print(f"Current fee: ${account.transaction_fee}")
account.transaction_fee = 2.00
print(f"New fee: ${account.transaction_fee}")

try:
    account.transaction_fee = -5  # Validation!
except ValueError as e:
    print(f"Error: {e}")

print("\n=== Using Methods ===")
print(account.deposit(500))
print(account.withdraw(200, "1234"))

print("\n=== Wrong PIN ===")
try:
    account.withdraw(100, "9999")
except ValueError as e:
    print(f"Error: {e}")

print(f"\nFinal balance: ${account.balance}")

# Demonstrate name mangling
print("\n=== Name Mangling (Private Attributes) ===")
print(f"Can't access __balance directly: {hasattr(account, '__balance')}")
print(f"But it's stored as _BankAccount__balance: {hasattr(account, '_BankAccount__balance')}")
print("(Don't do this in real code - defeats the purpose!)")
```

# BankAccount Class
# This solution demonstrates basic OOP concepts

class BankAccount:
    """A simple bank account class."""
    
    def __init__(self, account_holder, initial_balance=0):
        """Initialize account with holder name and optional balance."""
        self.account_holder = account_holder
        self.balance = initial_balance
    
    def deposit(self, amount):
        """Add money to the account."""
        if amount > 0:
            self.balance += amount
            print(f"Deposited ${amount:.2f}. New balance: ${self.balance:.2f}")
        else:
            print("Deposit amount must be positive")
    
    def withdraw(self, amount):
        """Withdraw money if sufficient balance."""
        if amount <= 0:
            print("Withdrawal amount must be positive")
            return False
        if amount > self.balance:
            print(f"Insufficient funds. Available: ${self.balance:.2f}")
            return False
        self.balance -= amount
        print(f"Withdrew ${amount:.2f}. New balance: ${self.balance:.2f}")
        return True
    
    def get_balance(self):
        """Return current balance."""
        return self.balance

# Create two accounts
alice_account = BankAccount("Alice", 1000)
bob_account = BankAccount("Bob", 500)

# Perform transactions
print(f"=== {alice_account.account_holder}'s Account ===")
alice_account.deposit(200)
alice_account.withdraw(150)

print(f"\n=== {bob_account.account_holder}'s Account ===")
bob_account.deposit(300)
bob_account.withdraw(1000)  # Should fail

# Print final balances
print("\n=== Final Balances ===")
print(f"{alice_account.account_holder}: ${alice_account.get_balance():.2f}")
print(f"{bob_account.account_holder}: ${bob_account.get_balance():.2f}")
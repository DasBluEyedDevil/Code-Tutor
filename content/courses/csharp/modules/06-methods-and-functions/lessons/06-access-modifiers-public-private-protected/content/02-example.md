---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
class BankAccount
{
    // PRIVATE: Only this class can access
    private decimal balance;
    private string accountNumber;
    
    // PUBLIC: Anyone can access
    public string AccountHolder;
    
    // PUBLIC constructor
    public BankAccount(string holder, decimal initialBalance)
    {
        AccountHolder = holder;
        balance = initialBalance;  // Can access private field within class
        accountNumber = GenerateAccountNumber();  // Calling private method
    }
    
    // PUBLIC method - users can call this
    public void Deposit(decimal amount)
    {
        if (amount > 0)
            balance += amount;
    }
    
    // PUBLIC method exposing private data safely
    public decimal GetBalance()
    {
        return balance;  // OK to access private field within class
    }
    
    // PRIVATE method - only internal use
    private string GenerateAccountNumber()
    {
        return "ACC" + new Random().Next(1000, 9999);
    }
}

// Usage
BankAccount account = new BankAccount("Alice", 1000);
account.Deposit(500);  // OK - public method
Console.WriteLine(account.GetBalance());  // OK - public method
// account.balance = 999999;  // ERROR! balance is private
// account.GenerateAccountNumber();  // ERROR! private method
```

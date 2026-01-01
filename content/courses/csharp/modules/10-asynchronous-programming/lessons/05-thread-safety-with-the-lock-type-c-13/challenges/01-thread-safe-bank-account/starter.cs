using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

class BankAccount
{
    private decimal _balance;
    private readonly Lock _lock = new();
    
    public BankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
    }
    
    public decimal Balance
    {
        get
        {
            // TODO: Make thread-safe
            return _balance;
        }
    }
    
    public void Deposit(decimal amount)
    {
        // TODO: Make thread-safe
        _balance += amount;
        Console.WriteLine($"Deposited {amount}, Balance: {_balance}");
    }
    
    public bool Withdraw(decimal amount)
    {
        // TODO: Make thread-safe, check for sufficient funds
        if (_balance >= amount)
        {
            _balance -= amount;
            Console.WriteLine($"Withdrew {amount}, Balance: {_balance}");
            return true;
        }
        return false;
    }
}

var account = new BankAccount(1000);
var tasks = new List<Task>();

Console.WriteLine($"Initial balance: {account.Balance}");

// Start deposit tasks
for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Run(() => account.Deposit(100)));
}

// Start withdraw tasks
for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Run(() => account.Withdraw(50)));
}

await Task.WhenAll(tasks);

Console.WriteLine($"\nFinal balance: {account.Balance}");
Console.WriteLine($"Expected: 1500");
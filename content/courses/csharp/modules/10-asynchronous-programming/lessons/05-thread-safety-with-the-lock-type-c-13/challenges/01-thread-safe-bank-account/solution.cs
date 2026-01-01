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
            lock (_lock)
            {
                return _balance;
            }
        }
    }
    
    public void Deposit(decimal amount)
    {
        lock (_lock)
        {
            _balance += amount;
            Console.WriteLine($"Deposited {amount}, Balance: {_balance}");
        }
    }
    
    public bool Withdraw(decimal amount)
    {
        lock (_lock)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew {amount}, Balance: {_balance}");
                return true;
            }
            Console.WriteLine($"Insufficient funds for {amount}");
            return false;
        }
    }
}

var account = new BankAccount(1000);
var tasks = new List<Task>();

Console.WriteLine($"Initial balance: {account.Balance}");
Console.WriteLine("Starting concurrent operations...\n");

for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Run(() => account.Deposit(100)));
}

for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Run(() => account.Withdraw(50)));
}

await Task.WhenAll(tasks);

Console.WriteLine($"\nFinal balance: {account.Balance}");
Console.WriteLine($"Expected: 1500");
Console.WriteLine(account.Balance == 1500 ? "SUCCESS!" : "Race condition detected!");